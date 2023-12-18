using Data;
using Data.Entity;
using Data.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Services.Common;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.Room;
using ViewModel.RoomDetails;
using ViewModel.RoomImage;

namespace Services.Implement
{
    public class RoomDetailsServices : IRoomDetails
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBaseControl<RoomImage> _base;
        private const string FolderName = @"../../../Images/ImageOfRoom/";

        public RoomDetailsServices(ApartmentDbContextFactory contextfactory, IBaseControl<RoomImage> baseControl)
        {
            _contextfactory = contextfactory;
            _base = baseControl;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(FolderName, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public async Task<DeleteImageViewModel> Delete(int id)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                DeleteImageViewModel delete = new DeleteImageViewModel
                {
                    result = 0,
                    url = null
                };
                RoomImage entity = await _context.RoomImage.FirstOrDefaultAsync((x) => x.ID == id);
                if (entity != null)
                {
                    delete.url = entity.Url;
                    _context.Remove(entity);

                    delete.result = await _context.SaveChangesAsync();
                    return delete;
                }
                return delete;
            }
        }

        public async Task<PagedResult<RoomDetailsFurniture>> GetAllFurniture(RequestPaging request, int id)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.RoomDetail
                            join pt in _context.Furniture on p.IDFur equals pt.ID
                            join px in _context.Room on p.IDRoom equals px.ID
                            select new { p, pt };
                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Where(x => x.p.IDRoom == id)
                    .Select(x => new RoomDetailsFurniture()
                    {
                        IdRoom = x.p.IDRoom,
                        IdFur = x.pt.ID,
                        NameFurniture = x.pt.Name,
                    }).ToListAsync();
                var pagedView = new PagedResult<RoomDetailsFurniture>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        public async Task<PagedResult<RoomDetailsImage>> GetAllImage(RequestPaging request, int id)
        {
            /*BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(new Uri(AppDomain.CurrentDomain.BaseDirectory), s);
            bitmapImage.EndInit();*/

            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.RoomImage
                            where p.IDroom == id
                            select p;
                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new RoomDetailsImage()
                    {
                        IDRoom = x.IDroom,
                        IDImage = x.ID,
                        Url = x.Url
                    }).ToListAsync();

                var pagedView = new PagedResult<RoomDetailsImage>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        public async Task<bool> CreateImage(RoomImageCreateViewModel model, string NameFile)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(NameFile)}";
            string resourcUri = FolderName + fileName;
            MessageBox.Show(resourcUri);
            try
            {
                System.IO.File.Copy(model.Url, resourcUri, true);
                RoomImage roomimage = new RoomImage
                {
                    IDroom = model.IDroom,
                    Name = model.Name,
                    Url = resourcUri
                };
                MessageBox.Show($"{roomimage.IDroom} {roomimage.Name} {roomimage.Url}");

                var result = await _base.Create(roomimage);
                if (result != null)
                {
                    return true;
                }
                else return false;
            }
            catch (IOException e)
            {
                MessageBox.Show($"Lỗi: {e.Message}");
                return false;
            }
        }

        public async Task<bool> CreateFurniture(RoomDetailsVm model)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var fur = new RoomDetails
                {
                    IDFur = model.IDFur,
                    IDRoom = model.IDRoom,
                };
                _context.RoomDetail.Add(fur);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteRoomFurniture(int id)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var result = await _context.RoomDetail.FirstOrDefaultAsync((x) => x.IDFur == id);
                if (result == null) return false;
                _context.RoomDetail.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<CustomerVM>> GetAllCustomerInRoom(int IDRoom)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.People
                            join pp in _context.RentalContract on p.ID equals pp.IDroom
                            join px in _context.PeopleRental on pp.ID equals px.IDRental
                            join pt in _context.Room on px.IDPeople equals pt.ID
                            where pt.ID == IDRoom
                            select p;
                var data = await query.Select(x => new CustomerVM()
                {
                    Name = x.Name
                }).ToListAsync();
                return data;
            }
        }
    }
}
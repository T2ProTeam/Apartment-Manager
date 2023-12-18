using AM.UI.Model;
using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ViewModel.Furniture;

namespace AM.UI.State.Store
{
    public class FurnitureStore
    {
        private readonly Apartment _apartment;
        private readonly List<FurnitureVm> _furniturevm;
        private readonly IFurniture _ifur;
        private Lazy<Task> _initialLazyFurniture;
        public List<FurnitureVm> fuuniturevm => _furniturevm;

        public event Action<FurnitureVm> FurnitureAdd;

        public event Action<bool> FurnitureDelete;

        public event Action<FurnitureVm> FurnitureUpdate;

        public FurnitureStore(Apartment apartment, IFurniture ifur)
        {
            _apartment = apartment;
            _furniturevm = new List<FurnitureVm>();
            _ifur = ifur;
            _initialLazyFurniture = new Lazy<Task>(InitializeFurniture);
        }

        private async Task InitializeFurniture()
        {
            List<FurnitureVm> furniture = await _apartment.GetAllFurniture();
            _furniturevm.Clear();
            _furniturevm.AddRange(furniture);
        }

        public async Task LoadFurniture()
        {
            try
            {
                await _initialLazyFurniture.Value;
            }
            catch (Exception)
            {
                _initialLazyFurniture = new Lazy<Task>(InitializeFurniture);
                throw;
            }
        }

        public async Task<Furniture> CreateFurniture(FurnitureCreateViewModel model)
        {
            var result = await _ifur.CreateFurniture(model);
            FurnitureVm create = new FurnitureVm
            {
                ID = result.ID,
                Name = result.Name,
            };
            _furniturevm.Add(create);
            FurnitureAdd?.Invoke(create);
            return result;
        }

        public async Task<bool> DeleteFurniture(int id)
        {
            var result = await _ifur.DeleteFurniture(id);
            _furniturevm.RemoveAll(x => x.ID == id);
            FurnitureDelete?.Invoke(result);
            return result;
        }

        public async Task<Furniture> UpdateFurniture(FurnitureUpdateViewModel model)
        {
            var result = await _ifur.UpdateFurniture(model);
            var update = new FurnitureVm()
            {
                ID = result.ID,
                Name = result.Name
            };
            var currentIndex = _furniturevm.FindIndex(x => x.ID == result.ID);
            if (currentIndex != -1)
            {
                _furniturevm[currentIndex] = update;
            }
            else
            {
                _furniturevm.Add(update);
            }
            FurnitureUpdate?.Invoke(update);
            return result;
        }
    }
}
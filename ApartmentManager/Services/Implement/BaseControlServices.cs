using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Services.Implement
{
    public class BaseControlServices<T> : IBaseControl<T> where T : DomainObject
    {
        private readonly ApartmentDbContextFactory _contextfactory;

        public BaseControlServices(ApartmentDbContextFactory contextfactory)
        {
            _contextfactory=contextfactory;
        }

        public async Task<T> Create(T entity)
        {
            using (AparmentDbContext context = _contextfactory.CreateDbContext())
            {
                EntityEntry<T> createentity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createentity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (AparmentDbContext context = _contextfactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((x) => x.ID==id);
                if (entity == null) return false;
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<T>> GetAll()
        {
            using (AparmentDbContext context = _contextfactory.CreateDbContext())
            {
                List<T> entitys = await context.Set<T>().ToListAsync();
                return entitys;
            }
        }

        public async Task<T> GetById(int id)
        {
            using (AparmentDbContext context = _contextfactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((x) => x.ID==id);
                if (entity == null) return null;
                return entity;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (AparmentDbContext context = _contextfactory.CreateDbContext())
            {
                entity.ID = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
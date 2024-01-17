using ImageGalleryAPI.Contracts;
using ImageGalleryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ImageGalleryAPI.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        private readonly DataContext _dataContext;
        internal DbSet<T> dbSet;

        public RepositoryBase(DataContext dataContext) 
        {
          _dataContext = dataContext;
            this.dbSet = _dataContext.Set<T>();
        }

        public async void Create(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
             dbSet.Remove(entity);
        }

        public T Get(int id)
        {
             return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
            
        }
        
    }
}

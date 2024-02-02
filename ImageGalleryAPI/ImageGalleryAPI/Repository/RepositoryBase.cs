using ImageGalleryAPI.Contracts;
using ImageGalleryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ImageGalleryAPI.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        private readonly DataContext _dataContext;
        private DbSet<T> _dbSet;

        public RepositoryBase(DataContext dataContext) 
        {
          _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public async void Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
             _dbSet.Remove(entity);
        }

        public T Get(int id)
        {
             return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            
        }
        
    }
}

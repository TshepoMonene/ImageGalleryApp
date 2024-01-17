using ImageGalleryAPI.Contracts;
using ImageGalleryAPI.Models;

namespace ImageGalleryAPI.Repository
{
    public class RepositoryManager:IRepositoryManager
    {
        private readonly IPhotosRepository _photosRepository;
        private readonly DataContext _dataContext;

        public RepositoryManager(IPhotosRepository photosRepository,DataContext dataContext)
        {
               _photosRepository = photosRepository;
               _dataContext = dataContext;
        }

        public IPhotosRepository Photos 
        {
           get { return _photosRepository; }
        }

        public int Save()
        {
            return _dataContext.SaveChanges();
        }
    }
}

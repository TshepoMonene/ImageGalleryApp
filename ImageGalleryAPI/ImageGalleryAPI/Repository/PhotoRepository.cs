using ImageGalleryAPI.Contracts;
using ImageGalleryAPI.Models;

namespace ImageGalleryAPI.Repository
{
    public class PhotoRepository:RepositoryBase<Photo>,IPhotosRepository
    {
        private readonly DataContext _dataContext;

        public PhotoRepository(DataContext dataContext) :base(dataContext)
        {
          _dataContext = dataContext;
        
        }
        
    }
}

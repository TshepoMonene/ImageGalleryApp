namespace ImageGalleryAPI.Contracts
{
    public interface IRepositoryManager
    {
        public IPhotosRepository Photos { get; }
        int Save();
    }
}

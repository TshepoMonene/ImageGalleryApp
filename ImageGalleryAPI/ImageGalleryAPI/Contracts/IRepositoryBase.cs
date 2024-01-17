namespace ImageGalleryAPI.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

using Microsoft.EntityFrameworkCore;

namespace ImageGalleryAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Photo> Photos { get; set; }
    }
}

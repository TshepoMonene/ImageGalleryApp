using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageGalleryAPI.Models
{
    public class DataContext : IdentityDbContext<UserModel>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Photo> Photos { get; set; }

       
    }
}

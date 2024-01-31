using ImageGalleryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace ImageGalleryAPI.SeedData
{
    public class SeedUsers
    {
        public static async Task SeedData(DataContext datacontext,
         UserManager<UserModel> userManager)
        {
            if(!userManager.Users.Any())
            {
                var users = new List<UserModel>
                {
                    new UserModel
                    {
                      
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new UserModel
                    {
                       
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new UserModel
                    {
                     
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            await datacontext.SaveChangesAsync();

        }
    }
}

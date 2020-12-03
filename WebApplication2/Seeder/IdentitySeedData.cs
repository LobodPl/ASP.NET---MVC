using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebApplication2.Seeder
{
    public class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Pa$$word1";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("Admin");
                IdentityResult result = await userManager.CreateAsync(user, adminPassword);
                System.Diagnostics.Debug.WriteLine(result);
            }
        }
    }
}
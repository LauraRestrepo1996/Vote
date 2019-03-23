

namespace Vote.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Vote.Web.Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();


            var user = await this.userHelper.GetUserByEmailAsync("maritzamunnoz7@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Laura",
                    LastName = "Restrepo",
                    Email = "maritzamunnoz7@gmail.com",
                    UserName = "maritzamunnoz7@gmail.com",
                    PhoneNumber = "3207535265"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Events.Any())
            {
                this.AddEvent("¿Estás de acuerdo con la nueva línea de metro al ITM Fraternidad?", user);
                this.AddEvent("¿Esta usted a favor del aborto?", user);
                this.AddEvent("¿Escuchas la radio?", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddEvent(string name, User user)
        {
            this.context.Events.Add(new Event 
            {
                Name = name,
                Description = "Descrition here",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                User = user
            });
        }



    }
}

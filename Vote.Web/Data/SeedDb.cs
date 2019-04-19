

namespace Vote.Web.Data
{
    using System;
    using System.Collections.Generic;
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
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            if (!this.context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Medellín" });
                cities.Add(new City { Name = "Bogotá" });
                cities.Add(new City { Name = "Calí" });
                cities.Add(new City { Name = "Pereira" });
                cities.Add(new City { Name = "Manizales" });
                cities.Add(new City { Name = "Yolombo" });
                cities.Add(new City { Name = "Amalfi" });

                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Colombia"
                });

                await this.context.SaveChangesAsync();
            }


           


            var user = await this.userHelper.GetUserByEmailAsync("maritzamunnoz7@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Laura",
                    LastName = "Restrepo",
                    Email = "maritzamunnoz7@gmail.com",
                    UserName = "maritzamunnoz7@gmail.com",
                    PhoneNumber = "3207535265",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()

                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
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

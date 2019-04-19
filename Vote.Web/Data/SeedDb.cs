

namespace Vote.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.CheckRoles();

            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }

            await this.CheckUserAsync("argemiro.621@outlook.es", "Argemiro", "Restrepo", "Customer");
            await this.CheckUserAsync("mariitza.rpo@gmail.com", "Maritza", "Muñoz", "Customer");
            var user = await this.CheckUserAsync("maritzamunnoz7@gmail.com", "Laura", "Restrepo", "Admin");

            // Add Events
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

        private async Task<User> CheckUserAsync(string userName, string firstName, string lastName, string role)
        {
            // Add user
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(userName, firstName, lastName, role);
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task<User> AddUser(string userName, string firstName, string lastName, string role)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
                UserName = userName,
                PhoneNumber = "350 634 2747",
                Gender = "No definido",
                Ocupattion = "Independiente",
                Birthdate = DateTime.Today,
                Stratum = "1",
                CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
            };

            var result = await this.userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);
            var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            await this.userHelper.ConfirmEmailAsync(user, token);
            return user;
        }

        private async Task AddCountriesAndCitiesAsync()
        {
            this.AddCountry("Colombia", new string[] { "Medellín", "Bogota", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira" });
            this.AddCountry("Argentina", new string[] { "Córdoba", "Buenos Aires", "Rosario", "Tandil", "Salta", "Mendoza" });
            this.AddCountry("Estados Unidos", new string[] { "New York", "Los Ángeles", "Chicago", "Washington", "San Francisco", "Miami", "Boston" });
            this.AddCountry("Ecuador", new string[] { "Quito", "Guayaquil", "Ambato", "Manta", "Loja", "Santo" });
            this.AddCountry("Peru", new string[] { "Lima", "Arequipa", "Cusco", "Trujillo", "Chiclayo", "Iquitos" });
            this.AddCountry("Chile", new string[] { "Santiago", "Valdivia", "Concepcion", "Puerto Montt", "Temucos", "La Sirena" });
            this.AddCountry("Uruguay", new string[] { "Montevideo", "Punta del Este", "Colonia del Sacramento", "Las Piedras" });
            this.AddCountry("Bolivia", new string[] { "La Paz", "Sucre", "Potosi", "Cochabamba" });
            this.AddCountry("Venezuela", new string[] { "Caracas", "Valencia", "Maracaibo", "Ciudad Bolivar", "Maracay", "Barquisimeto" });
            this.AddCountry("Paraguay", new string[] { "Asunción", "Ciudad del Este", "Encarnación", "San  Lorenzo", "Luque", "Areguá" });
            this.AddCountry("Brasil", new string[] { "Rio de Janeiro", "São Paulo", "Salvador", "Porto Alegre", "Curitiba", "Recife", "Belo Horizonte", "Fortaleza" });
            this.AddCountry("Panamá", new string[] { "Chitré", "Santiago", "La Arena", "Agua Dulce", "Monagrillo", "Ciudad de Panamá", "Colón", "Los Santos" });
            this.AddCountry("México", new string[] { "Guadalajara", "Ciudad de México", "Monterrey", "Ciudad Obregón", "Hermosillo", "La Paz", "Culiacán", "Los Mochis" });
            await this.context.SaveChangesAsync();
        }

        private void AddCountry(string country, string[] cities)
        {
            var theCities = cities.Select(c => new City { Name = c }).ToList();
            this.context.Countries.Add(new Country
            {
                Cities = theCities,
                Name = country
            });
        }

        private async Task CheckRoles()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
        }

       
    }

}

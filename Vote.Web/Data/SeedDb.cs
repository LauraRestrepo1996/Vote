

namespace Vote.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class SeedDb
    {
        private readonly DataContext context;


        public SeedDb(DataContext context)
        {
            this.context = context;

        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Events.Any())
            {
                this.AddEvent("¿Estás de acuerdo con la nueva línea de metro al ITM Fraternidad?");
                this.AddEvent("¿Esta usted a favor del aborto?");
                this.AddEvent("¿Escuchas la radio?");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddEvent(string name)
        {
            this.context.Events.Add(new Event
            {
                Name = name,
               // Description = "Descrition here",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            });
        }



    }
}

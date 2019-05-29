
namespace Vote.Common.ViewModels
{
    using System.Collections.Generic;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Models.Services;
    using System.Linq;
    using MvvmCross.Commands;
    using System.Windows.Input;
    using MvvmCross.Navigation;

    public class EventsViewModel : MvxViewModel
    {
        private List<Event> events;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private MvxCommand<Event> itemClickCommand;
        private MvxCommand addEventCommand;
        private readonly IMvxNavigationService navigationService;


        public List<Event> Events
        {
            get => this.events;
            set => this.SetProperty(ref this.events, value);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.LoadEvents();
        }

        public EventsViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.LoadEvents();
            this.navigationService = navigationService;
        }

        public ICommand ItemClickCommand
        {
            get
            {
                this.itemClickCommand = new MvxCommand<Event>(this.OnItemClickCommand);
                return itemClickCommand;
            }
        }

        public ICommand AddEventCommand
        {
            get
            {
                this.addEventCommand = this.addEventCommand ?? new MvxCommand(this.AddEvent);
                return this.addEventCommand;
            }
        }


        private async void LoadEvents()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<Event>(
                "https://votemunnoz.azurewebsites.net",
                "/api",
                "/Events",
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Events = (List<Event>)response.Result;
            this.Events = this.Events.OrderBy(E => E.Name).ToList();

    }
           


        private async void AddEvent()
        {
            await this.navigationService.Navigate<AddEventViewModel>();
        }

        private async void OnItemClickCommand(Event @event)
        {
            await this.navigationService.Navigate<EventsDetailViewModel, NavigationArgs>
                (new NavigationArgs { Event = @event });
        }


    }

}

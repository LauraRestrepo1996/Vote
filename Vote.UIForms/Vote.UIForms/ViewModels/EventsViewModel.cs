

namespace Vote.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Vote.Common.Models;
    using System.Linq;
    using Vote.Common.Models.Services;
    using Xamarin.Forms;
    public class EventsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private List<Event> myEvents;
        private ObservableCollection<Event> events;
        private bool isRefreshing;

        public ObservableCollection<Event> Events
        {
            get => this.events;
            set => this.SetValue(ref this.events, value);
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }

        public EventsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadEvents();
        }

        private async void LoadEvents()
        {
            this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Event>(
                url,
                "/api",
                "/Events",
                "bearer",
                MainViewModel.GetInstance().Token.Token);


            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            this.myEvents = (List<Event>)response.Result;
            this.events = new ObservableCollection<Event>(myEvents);
        }

        public void AddEventsToList(Event @event)
        {
            this.myEvents.Add(@event);
            this.events = new ObservableCollection<Event>(myEvents);
        }

        public void DeleteEventList(int IdEvent)
        {
            var previousEvent = this.myEvents.Where(p => p.Id == IdEvent).FirstOrDefault();
            if (previousEvent != null)
            {
                this.myEvents.Remove(previousEvent);
            }

            this.events = new ObservableCollection<Event>(myEvents);
        }

        public void UpdateEventList(Event @event)
        {
            var previousReference = this.myEvents.Where(p => p.Id == @event.Id).FirstOrDefault();
            if (previousReference != null)
            {
                this.myEvents.Remove(previousReference);
            }

            this.myEvents.Add(@event);
            this.events = new ObservableCollection<Event>(myEvents);
        }


    }
}


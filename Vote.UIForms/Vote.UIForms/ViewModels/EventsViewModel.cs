﻿

namespace Vote.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Vote.Common.Models;
    using Vote.Common.Models.Services;
    using Xamarin.Forms;
    public class EventsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<Event> events;
        private bool isRefreshing;

        public ObservableCollection<Event> Events
        {
            get { return this.events; }
            set { this.SetValue(ref this.events, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public EventsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadEvents();
        }

        private async void LoadEvents()
        {
            this.IsRefreshing = true;
            var response = await this.apiService.GetListAsync<Event>(
                "https://voterestrepo.azurewebsites.net",
                "/api",
                "/Events");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var events = (List<Event>)response.Result;
            this.events = new ObservableCollection<Event>(events);
        }
    }
}
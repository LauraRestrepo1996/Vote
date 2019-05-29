

namespace Vote.Common.ViewModels
{
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Models.Services;
    using System.Collections.Generic;

    public class EventsDetailViewModel : MvxViewModel<NavigationArgs>
    {
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;

        //private Event @event;
        private List<Candidate> candidates;
        private Candidate candidate;
        private bool isLoading;
        private MvxCommand updateCommand;
        private MvxCommand deleteCommand;

        public EventsDetailViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.IsLoading = false;
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public List<Candidate> Candidate
        {
            get => this.candidates;
            set => this.SetProperty(ref this.candidates, value);
        }

        //public Event Event
        //{
        //    get => this.@event;
        //    set => this.SetProperty(ref this.@event, value);
        //}

        //public Candidate Candidate
        //{
        //    get => this.candidate;
        //    set => this.SetProperty(ref this.candidate, value);
        //}

        public ICommand UpdateCommand
        {
            get
            {
                this.updateCommand = this.updateCommand ?? new MvxCommand(this.Update);
                return this.updateCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                this.deleteCommand = this.deleteCommand ?? new MvxCommand(this.Delete);
                return this.deleteCommand;
            }
        }



        private async void Delete()
        {
            //TODO: Ask for confirmation
            this.IsLoading = true;

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await this.apiService.DeleteAsync (
                "https://votemunnoz.azurewebsites.net",
                "/api",
                "/Candidates",
                //@event.Id,
                candidate.Id,
                "bearer",
                token.Token);

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            await this.navigationService.Close(this);
        }

        private async void Update()
        {
            //if (string.IsNullOrEmpty(this.Candidate.name))
            //{
            //    this.dialogService.Alert("Error", "You must enter a candidate name.", "Accept");
            //    return;
            //}

         

            this.IsLoading = true;

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await this.apiService.PutAsync (
                "https://votemunnoz.azurewebsites.net",
                "/api",
                "/Candidates",
                //@event.Id,
                candidate,
                "bearer",
                token.Token);

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            await this.navigationService.Close(this);
        }

        //public override void Prepare(NavigationArgs parameter)
        //{
        //    this.@event = parameter.Event;
        //}

        public override void Prepare(NavigationArgs parameter)
        {
            this.candidate = parameter.Candidate;
        }

      

    }

}

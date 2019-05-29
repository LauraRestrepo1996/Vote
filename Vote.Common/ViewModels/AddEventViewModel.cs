

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
    using System;

    class AddEventViewModel : MvxViewModel
    {
        private string name;
        private DateTime endDate;
        private DateTime startDate;
        private string description;
        private MvxCommand addProductCommand;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private bool isLoading;

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        public DateTime EndDate
        {
            get => this.endDate;
            set => this.SetProperty(ref this.endDate, value);
        }

        public DateTime StartDate
        {
            get => this.startDate;
            set => this.SetProperty(ref this.startDate, value);
        }

        public string Description
        {
            get => this.description;
            set => this.SetProperty(ref this.description, value);
        }

        public ICommand AddProductCommand
        {
            get
            {
                this.addProductCommand = this.addProductCommand ?? new MvxCommand(this.AddProduct);
                return this.addProductCommand;
            }
        }

        public AddEventViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

        private async void AddProduct()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                this.dialogService.Alert("Error", "You must enter a product name.", "Accept");
                return;
            }

          

            this.IsLoading = true;

            //TODO: Image pending
            var @event = new Event
            {

                Name = this.Name,
                EndDate = this.EndDate,
                StartDate = this.StartDate,
                Description = this.Description,
                User = new User { UserName = Settings.UserEmail },
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

        //    var response = await this.apiService.PostAsync(
        //        "https://votemunnoz.azurewebsites.net",
        //        "/api",
        //        "/Events",
        //        @event,
        //        "bearer",
        //        token.Token);

        //    this.IsLoading = false;

        //    if (!response.IsSuccess)
        //    {
        //        this.dialogService.Alert("Error", response.Message, "Accept");
        //        return;
        //    }

          
        //    await this.navigationService.Close(this);
        }
    }

}

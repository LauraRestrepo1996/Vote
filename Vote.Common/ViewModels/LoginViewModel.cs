

namespace Vote.Common.ViewModels
{
    using System.Windows.Input;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.ViewModels;
    
    using Models.Services;
    using MvvmCross.Navigation;
    using Newtonsoft.Json;
    using Vote.Common.Helpers;

    public class LoginViewModel : MvxViewModel
    {
        private string email;
        private string password;
        private MvxCommand loginCommand;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private MvxCommand registerCommand;

        private bool isLoading;

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public ICommand LoginCommand
        {
            get
            {
                this.loginCommand = this.loginCommand ?? new MvxCommand(this.DoLoginCommand);
                return this.loginCommand;
            }
        }

        public LoginViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            this.Email = "mariitza.rpo@gmail.com";
            this.Password = "123456";
            this.IsLoading = false;
        }

        private async void DoLoginCommand()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.dialogService.Alert("Error", "You must enter a password.", "Accept");
                return;
            }

            this.IsLoading = true;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var response = await this.apiService.GetTokenAsync(
                "https://votemunnoz.azurewebsites.net",
                "/Account",
                "/CreateToken",
                request);

            if (!response.IsSuccess)
            {
                this.IsLoading = false;
                this.dialogService.Alert("Error", "User or password incorrect.", "Accept");
                return;
            }

            //this.IsLoading = false;
            //this.dialogService.Alert("Ok", "Welcome", "Accept");

            var token = (TokenResponse)response.Result;
            var response2 = await this.apiService.GetUserByEmailAsync(
           "https://votemunnoz.azurewebsites.net",
           "/api",
           "/Account/GetUserByEmail",
           this.Email,
           "bearer",
           token.Token);

            var user = (User)response2.Result;
            Settings.UserPassword = this.Password;
            Settings.User = JsonConvert.SerializeObject(user);
            Settings.UserEmail = this.Email;
            Settings.Token = JsonConvert.SerializeObject(token);
            this.IsLoading = false;

            //this.dialogService.Alert("Ok", "Fuck yeah!", "Accept");
            await this.navigationService.Navigate<EventsViewModel>();

        }

        public ICommand RegisterCommand
        {
            get
            {
                this.registerCommand = this.registerCommand ?? new MvxCommand(this.DoRegisterCommand);
                return this.registerCommand;
            }
        }

        private async void DoRegisterCommand()
        {
            await this.navigationService.Navigate<RegisterViewModel>();
        }

    }


}

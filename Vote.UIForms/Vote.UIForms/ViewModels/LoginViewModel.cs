

namespace Vote.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
   // using Vote.Common.Models;
   // using Shop.Common.Services;
    using Vote.UIForms.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel
    {
       
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);


        public LoginViewModel()
        {
            //this.apiService = new ApiService();
            this.Email = "maritzamunnoz7@gmail.com";
            this.Password = "123456";
            //this.IsEnabled = true;
        }

        private async void Login()
        {
            if(string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Your must enter an Email",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Your must enter an Password",
                    "Accept");
                return;
            }

            if (!Email.Equals("maritzamunnoz7@gmail.com") || !this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "User or password wrong.",
                   "Accept");
                return;
            }

            //await Application.Current.MainPage.DisplayAlert(
            //        "OK",
            //        "To welcome system Voting",
            //        "Accept");

            await Application.Current.MainPage.Navigation.PushAsync(new EventsPage());
            MainViewModel.GetInstance().Events = new EventsViewModel();
        }
    }
}

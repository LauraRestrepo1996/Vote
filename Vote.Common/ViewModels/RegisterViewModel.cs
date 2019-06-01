

namespace Vote.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Models.Services;
    using System;
    using Vote.Common.Helpers;

    public class RegisterViewModel : MvxViewModel
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly IDialogService dialogService;
        private List<Country> countries;
        private List<City> cities;
        private Country selectedCountry;
        private City selectedCity;
        private MvxCommand registerCommand;
        private string firstName;
        private string lastName;
        private string email;
        private string address;
        private string phone;
        private string password;
        private DateTime birthdate;
        private string gender;
        private string occupation;
        private string stratum;
        private string confirmPassword;
        private bool isLoading;

        public RegisterViewModel(
            IMvxNavigationService navigationService,
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.LoadCountries();
            this.IsLoading = false;
        }

        public ICommand RegisterCommand
        {
            get
            {
                this.registerCommand = this.registerCommand ?? new MvxCommand(this.RegisterUser);
                return this.registerCommand;
            }
        }

        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }

        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }
        

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

        public string Address
        {
            get => this.address;
            set => this.SetProperty(ref this.address, value);
        }

        public string Phone
        {
            get => this.phone;
            set => this.SetProperty(ref this.phone, value);
        }

        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public string ConfirmPassword
        {
            get => this.confirmPassword;
            set => this.SetProperty(ref this.confirmPassword, value);
        }

        public string Gender
        {
            get => this.gender;
            set => this.SetProperty(ref this.gender, value);
        }

    public string Occupation
        {
            get => this.occupation;
            set => this.SetProperty(ref this.occupation, value);
    }


    public DateTime Birthdate
        {
            get => this.Birthdate;
            set => this.SetProperty(ref this.birthdate, value);
    }


    public string Stratum
        {
            get => this.stratum;
            set => this.SetProperty(ref this.stratum, value);
    }


    public List<Country> Countries
        {
            get => this.countries;
            set => this.SetProperty(ref this.countries, value);
        }

        public List<City> Cities
        {
            get => this.cities;
            set => this.SetProperty(ref this.cities, value);
        }

        public Country SelectedCountry
        {
            get => selectedCountry;
            set
            {
                this.selectedCountry = value;
                this.RaisePropertyChanged(() => SelectedCountry);
                this.Cities = SelectedCountry.Cities;
            }
        }

        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                RaisePropertyChanged(() => SelectedCity);
            }
        }

        private async void LoadCountries()
        {
            this.IsLoading = true;
            var response = await this.apiService.GetListAsync<Country>(
                "https://shopzulu.azurewebsites.net",
                "/api",
                "/Countries");
            this.IsLoading = false;
            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Countries = (List<Country>)response.Result;
        }

        private async void RegisterUser()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                this.dialogService.Alert("Error", "You must enter a first name.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                this.dialogService.Alert("Error", "You must enter a last name.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (!RegexHelper.IsValidEmail(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter a valid email.", "Accept");
                return;
            }


            if (string.IsNullOrEmpty(this.Phone))
            {
                this.dialogService.Alert("Error", "You must enter a phone.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.dialogService.Alert("Error", "You must enter a password.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Gender))
            {
                this.dialogService.Alert("Error", "You must enter a Gender.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Occupation))
            {
                this.dialogService.Alert("Error", "You must enter a Occupation.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Stratum))
            {
                this.dialogService.Alert("Error", "You must enter a Stratum.", "Accept");
                return;
            }

            if (this.Birthdate == null)
            {
                this.dialogService.Alert("Error", "You must enter a Birthdate.", "Accept");
                return;
            }
            if (this.Password.Length < 6)
            {
                this.dialogService.Alert("Error", "The password must be a least 6 characters.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.ConfirmPassword))
            {
                this.dialogService.Alert("Error", "You must enter a pasword confirm.", "Accept");
                return;
            }

            if (!this.Password.Equals(this.ConfirmPassword))
            {
                this.dialogService.Alert("Error", "The pasword and confirm does not math.", "Accept");
                return;
            }

            this.IsLoading = true;

            var request = new NewUserRequest
            {

                CityId = this.SelectedCity.Id,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Password = this.Password,
                Phone = this.Phone,
                Birthdate = this.Birthdate,
                Gender = this.Gender,
                Ocupattion = this.Occupation ,
                Stratum = this.Stratum,
                
            };

            var response = await this.apiService.RegisterUserAsync(
                "https://votemunnoz.azurewebsites.net",
                "/api",
                "/Account",
                request);

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }


            this.dialogService.Alert(
         "Ok",
         "The user was created succesfully, you must " +
         "confirm your user by the email sent to you and then you could login with " +
         "the email and password entered.",
         "Accept",
         () => { this.navigationService.Close(this); }); // Delegado, Aim to the funcion

        }

    }

}

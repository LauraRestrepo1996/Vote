﻿

namespace Vote.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Vote.Common.Helpers;
    using Vote.Common.Models.Services;
    using Vote.UIForms.Helpers;
    using Xamarin.Forms;

    public class RegisterViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Country> countries;
        private Country country;
        private ObservableCollection<City> cities;
        private City city;
        private readonly ApiService apiService;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Ocupattion { get; set; }

        public string Stratum { get; set; }

        public string Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }

        public Country Country
        {
            get => this.country;
            set
            {
                this.SetValue(ref this.country, value);
                this.Cities = new ObservableCollection<City>(this.Country.Cities.OrderBy(c => c.Name));
            }

        }

        public City City
        {
            get => this.city;
            set => this.SetValue(ref this.city, value);
        }

        public ObservableCollection<Country> Countries
        {
            get => this.countries;
            set => this.SetValue(ref this.countries, value);
        }

        public ObservableCollection<City> Cities
        {
            get => this.cities;
            set => this.SetValue(ref this.cities, value);
        }

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public ICommand RegisterCommand => new RelayCommand(this.Register);

        public RegisterViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.LoadCountries();

        }

        private async void LoadCountries()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Country>(
                url,
                "/api",
                "/Countries");

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    response.Message,
                     Languages.Accept);
                return;
            }

            var myCountries = (List<Country>)response.Result;
            this.Countries = new ObservableCollection<Country>(myCountries);
        }


        private async void Register()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.FirstNameEnter,
                     Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.LastNameEnter,
                     Languages.Accept);
                return;
            }

          

            if (!RegexHelper.IsValidEmail(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.EmailEnter,
                     Languages.Accept);
                return;
            }

            if (this.Country == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.Country,
                    Languages.Accept);
                return;
            }

            if (this.City == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.SelectCity,
                     Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Ocupattion))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.OcupattionEnter,
                     Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Stratum))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.StratumEnter,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Gender))
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                    Languages.GenderEnter,
                    Languages.Accept);
                return;
            }



            if (string.IsNullOrEmpty(this.Phone))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    "You must enter a phone number.",
                     Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.Password,
                     Languages.Accept);
                return;
            }

            if (this.Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordMin,
                     Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.PasswordConfirm,
                     Languages.Accept);
                return;
            }

            if (!this.Password.Equals(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.PasswordMatch,
                     Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new NewUserRequest
            {
                
                CityId = this.City.Id,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Password = this.Password,
                Phone = this.Phone,
                Ocupattion = this.Ocupattion,
                Gender = this.Gender,
                Stratum = this.Stratum,
                Birthdate = this.Birthdate


            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.RegisterUserAsync(
                url,
                "/api",
                "/Account",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;



            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                Languages.Ok,
                response.Message,
                 Languages.Accept);


            await Application.Current.MainPage.Navigation.PopAsync();
        }


    }

}

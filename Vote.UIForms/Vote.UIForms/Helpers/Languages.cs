using System;
using System.Collections.Generic;
using System.Text;

namespace Vote.UIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string EmailError => Resource.EmailError;
        

        public static string EmPasError => Resource.EmPasError;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordEnter => Resource.PasswordEnter;

        public static string EmailEnter => Resource.EmailEnter;

        public static string Remember => Resource.Remember;

        public static string Email => Resource.Email;

        public static string Password => Resource.Password;

        public static string Login => Resource.Login;

    }

}

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

        public static string Ocupattion => Resource.Ocupattion;
        public static string Occupation => Resource.Occupation;
        public static string OcupattionEnter => Resource.OcupattionEnter;

        public static string Stratum => Resource.Stratum;

        public static string StratumEnter => Resource.StratumEnter;

        public static string Gender => Resource.Gender;

        public static string GenderEnter => Resource.GenderEnter;

        public static string Birthdate => Resource.Birthdate;

        public static string BirthdateEnter => Resource.BirthdateEnter;

        public static string FirstName => Resource.FirstName;

        public static string LastName => Resource.LastName;

        public static string City => Resource.City;

        public static string SelectCity => Resource.SelectCity;

        public static string Country => Resource.Country;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordEnterConf => Resource.PasswordEnterConf;

        public static string Register => Resource.Register;

        public static string FirstNameEnter => Resource.FirstNameEnter;

        public static string LastNameEnter => Resource.LastNameEnter;

        public static string Ok => Resource.Ok;

        public static string PasswordMin => Resource.PasswordMin;

        public static string PasswordMatch => Resource.PasswordMatch;

        public static string EmailValid => Resource.EmailValid;

        public static string RecoverPassword => Resource.RecoverPassword;

        public static string EmailRecoverPassword => Resource.EmailRecoverPassword;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string NewPassword => Resource.NewPassword;
        public static string EnterNewPassword => Resource.EnterNewPassword;
        public static string CurrentPassword => Resource.CurrentPassword;
        public static string EnterCurrentPassword => Resource.EnterCurrentPassword;
        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;
        public static string RenterPassword => Resource.RenterPassword;

        public static string ChangePassword => Resource.ChangePassword;

        public static string Events => Resource.Events;

        public static string Menu => Resource.Menu;

        public static string Welcome => Resource.Welcome;

        public static string ModifyPassword => Resource.ModifyPassword;

        public static string ModifyUser => Resource.ModifyUser;

        public static string Save => Resource.Save;

        public static string Phone => Resource.Phone;

        public static string PhoneEnter => Resource.PhoneEnter;

        public static string Registered => Resource.Registered; 
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using Vote.Common.Models;

namespace Vote.UIForms.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public EventsViewModel Events { get; set; }

        private static MainViewModel instance; // Apuntador

        public TokenResponse Token { get; set; }
        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}

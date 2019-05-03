using MeteoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MeteoApp.ViewModels
{

    class RegistryViewModel : BaseViewModel
    {
        User _user;
        string _password;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        public RegistryViewModel( User user, ref string password)
        {
            User = user;
            Password = password;
        }

    }
}

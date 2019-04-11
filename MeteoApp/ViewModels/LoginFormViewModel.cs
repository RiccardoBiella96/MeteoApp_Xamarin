using MeteoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoApp.ViewModels
{
    
    class LoginFormViewModel: BaseViewModel
    {
        User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public LoginFormViewModel(User user)
        {
            User = user;
        }

    }
}

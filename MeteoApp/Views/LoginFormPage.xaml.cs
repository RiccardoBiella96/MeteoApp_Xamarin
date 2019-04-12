using MeteoApp.Models;
using MeteoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoApp.Views
{
	public partial class LoginFormPage : ContentPage
	{
        User user = new User();
        public LoginFormPage ()
		{
			InitializeComponent ();

            
            BindingContext = new LoginFormViewModel(user);
		}


        void onRegistryClicked(object sender, EventArgs e)
        {
            DisplayAlert("Informazione", "Stai creando un utente", "OK");
            Navigation.PushAsync(new RegistryPage()
            {
                
            });
        }
        void onAuthenticateClicked(object sender, EventArgs e)
        {
            //chiedere al server la correttezza delle credenziali
            
        }
    }
}
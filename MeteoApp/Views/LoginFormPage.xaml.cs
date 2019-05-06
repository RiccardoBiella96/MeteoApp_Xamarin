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
using System.Net.Http;

namespace MeteoApp.Views
{
	public partial class LoginFormPage : ContentPage
	{
        User user = new User();
        MeteoListViewModel mModel;

        public LoginFormPage (Object m)
		{
            mModel = (MeteoListViewModel)m;
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
        async void onAuthenticateClicked(object sender, EventArgs e)
        {
            //chiedere al server la correttezza delle credenziali
            bool result = await postRequestLogin();

            //TODO controllare esito e andare alla pagina di aggiunta località
            Console.WriteLine("RESULT async task: " + result);


            if (result)
            {
                //fare partire la dialog di aggiunta citta
                DisplayAlert("Informazione", "Log in eseguito", "OK");
                //Page page = await Navigation.PopAsync();
                //Navigation.PushAsync(page);
                await Navigation.PopToRootAsync();
            }
            else
            {
                //visualizzare label dati login non validi
                // alertCredentials.IsVisible = true;
                alertCredentials.IsVisible = true;
            }
            
        }

        private async Task<bool> postRequestLogin()
        {

            var userSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //Console.WriteLine("Stringa json"+userSerialized);
            var httpClient = new HttpClient();
            var result = await httpClient.PostAsync("http://10.11.89.182:8080/public/login", new StringContent(userSerialized, Encoding.UTF8, "application/json"));
            XAuthKey.xauth = "";
            string resultcontent = await result.Content.ReadAsStringAsync();
            Console.WriteLine("RESULT: " + resultcontent);
            
            try
            {
                XAuthKey.xauth = result.Headers.GetValues("X-Auth").FirstOrDefault();
                Console.WriteLine("HEADER: " + XAuthKey.xauth);
                if(mModel != null)
                    mModel.getUserLocations();
                return true;
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
        }
    }
}
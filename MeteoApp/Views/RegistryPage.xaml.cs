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
using System.Net.Http.Headers;
using System.Net.Http;

namespace MeteoApp.Views
{
	public partial class RegistryPage : ContentPage
	{
        User user=new User();

        string password;
        


        public RegistryPage ()
		{
			InitializeComponent ();


            //BindingContext = this;

            AreCredentialsInvalid.IsVisible = false;
           


            BindingContext = new RegistryViewModel(user, ref password);
        }



        async void verifyCredential(object sender, EventArgs e)
        {
            var temp = BindingContext as RegistryViewModel;
            Console.WriteLine(user.password);
            Console.WriteLine(password); 
            password = p.Text;
            var a = user.password.Equals(password);

            if (string.Equals(user.password, password))
            {
                
                AreCredentialsInvalid.IsVisible = false;
                await postRequestRegistry();
                Console.WriteLine("ho registrato");
                //TODO devo ritornare alla schermata di login come faccio?
                Navigation.PushAsync(new LoginFormPage()

                {
                });
            }
            else
            {
                AreCredentialsInvalid.IsVisible = true;
            }
            
        }

        

        private async Task postRequestRegistry()
        {
            
            var userSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //Console.WriteLine("Stringa json"+userSerialized);
            var httpClient = new HttpClient();
            var result = await httpClient.PostAsync("http://10.11.89.182:8080/public/register",new StringContent(userSerialized, Encoding.UTF8, "application/json"));

            string resultcontent = await result.Content.ReadAsStringAsync();
            Console.WriteLine("RESULT: " + resultcontent);
  
        }
    }
}
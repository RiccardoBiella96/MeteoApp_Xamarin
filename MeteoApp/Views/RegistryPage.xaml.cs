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

        

        void verifyCredential(object sender, EventArgs e)
        {
            var temp = BindingContext as RegistryViewModel;
            Console.WriteLine(user.password);
            Console.WriteLine(password);

            if (string.Equals(user.password, password))
            {
                Console.WriteLine("ENTRANCE CONDITION: SONO ENTRATO");
                AreCredentialsInvalid.IsVisible = false;
                postRequestRegistry();
                //TODO devo ritornare alla schermata di login come faccio?
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
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MeteoApp.Models;
using MeteoApp.Views;
using Xamarin.Forms;

namespace MeteoApp
{
    public partial class MeteoListPage : ContentPage
    {
        public MeteoListPage()
        {
            InitializeComponent();
            BindingContext = new MeteoListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            bool result = await XAuthKey.isValid();
            Console.WriteLine("VALORE RESULT:" + result);
            if (!result)
            {
                DisplayAlert("Attenzione", "E' necessario eseguire il login per procedere", "OK");
                Navigation.PushAsync(new LoginFormPage(BindingContext)
                {
                });
                //((MeteoListViewModel)BindingContext).getUserLocations();
            }
            else
            {
                PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
                {
                    InputType = InputType.Default,
                    OkText = "Add",
                    Title = "Add location",
                });

                if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
                {
                    OpenWeatherMap openWeatherMap = new OpenWeatherMap();
                    Entry entry = new Entry();
                    entry.Name = pResult.Text;
                    Task<Entry> task = openWeatherMap.UpdateWeatherInfo(entry);
                    var newEntry = await task;
                    ((MeteoListViewModel) BindingContext).addNewEntry(newEntry);

                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("X-Auth", XAuthKey.xauth);
                    var res = await httpClient.PostAsync("http://10.11.89.182:8080/protected/addLocation/" + entry.Name,
                        new StringContent("Add new Location", Encoding.UTF8, "text/html"));
                    string resultcontent = await res.Content.ReadAsStringAsync();
                    Console.WriteLine("res adding location: " + resultcontent);
                }
            }
        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage()
                {
                    BindingContext = new MeteoItemViewModel(e.SelectedItem as Entry)
                });
            }
        }
    }
}
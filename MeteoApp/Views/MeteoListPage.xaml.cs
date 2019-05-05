using MeteoApp.Models;
using MeteoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            DisplayAlert("Attenzione", "Stai per entrare in un zona protetta", "OK");
            bool result = await XAuthKey.isValid();
            Console.WriteLine("VALORE RESULT:"+ result);
            if (!result)
            {
                
                Navigation.PushAsync(new LoginFormPage()

                {
                });
            }
            else
            {
                Navigation.PushAsync(new AddCityPage()

                {
                });
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

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MeteoApp
{
    public partial class App : Application
    {

        static db.DBManager database;

        public static db.DBManager Database
        {
            get
            {
                if (database == null)
                    database = new db.DBManager();
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            var nav = new NavigationPage(new MeteoListPage())
            {
                BarBackgroundColor = Color.Gray,
                BarTextColor = Color.White
            };

            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            try{
                Models.XAuthKey.xauth = App.Database.GetItemsWithWhere("token").Result[0].value;
                Console.WriteLine("CI SONO RIUSCITO! " + Models.XAuthKey.xauth);

            }
            catch (Exception e) { }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Models.EntryToSave t = new Models.EntryToSave();
            t.key = "token";
            t.value = Models.XAuthKey.xauth;
            Console.WriteLine("ONSLEEP: " + Models.XAuthKey.xauth);
            App.Database.SaveItemAsync(t);

        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using System;
using System.IO;
using PM2E3P201820110061.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E3P201820110061
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.PagoViews());
        }

        static DataBaseSQLite dataBaseE3P0061;

        public static DataBaseSQLite BaseDatos
        {
            get
            {
                if (dataBaseE3P0061 == null)
                    dataBaseE3P0061 = new DataBaseSQLite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PM2E3P0061.db3"));

                return dataBaseE3P0061;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

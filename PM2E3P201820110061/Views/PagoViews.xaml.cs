using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PM2E3P201820110061.Views
{
    public partial class PagoViews : ContentPage
    {
        public PagoViews()
        {
            InitializeComponent();            
        }

        private async void btnVerLista_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Views.PagoListaViews());
        }
    }
}

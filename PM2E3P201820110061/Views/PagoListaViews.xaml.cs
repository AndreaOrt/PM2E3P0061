using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PM2E3P201820110061.Views
{
    public partial class PagoListaViews : ContentPage
    {
        public PagoListaViews()
        {
            InitializeComponent();
            BindingContext = new ViewModels.PagosListaViewModel(Navigation);
        }
    }
}

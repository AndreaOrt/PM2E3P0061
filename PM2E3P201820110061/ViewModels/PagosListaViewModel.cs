using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PM2E3P201820110061.Models;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace PM2E3P201820110061.ViewModels
{
    public class PagosListaViewModel: BaseViewModel
    {
        public PagosListaViewModel(INavigation navigation)
        {
            Navigation = navigation;

            ListadoPagos();

            EditarPagoCommand = new Command<Type>(async (pageType) => await EditarPago(pageType));
            EliminarPagoCommand = new Command(() => EliminarPago());
        }

        async void ListadoPagos()
        {
            PagosL = new ObservableCollection<Pagos>();
            var pagoLista = await App.BaseDatos.ObtenerListaPago();

            foreach (var e in pagoLista)
            {
                PagosL.Add(new Models.Pagos() { Id_pago = e.Id_pago, Descripcion = e.Descripcion, Monto = e.Monto, Fecha = e.Fecha, Photo_recibo = e.Photo_recibo });
            }
        }

        private ObservableCollection<Pagos> _pagos;

        public ObservableCollection<Pagos> PagosL
        {
            get { return _pagos; }
            set { _pagos = value; OnPropertyChange(); }
        }

        private Pagos _selectedPago;

        public Pagos SelectedPago
        {
            get { return _selectedPago; }
            set { _selectedPago = value; OnPropertyChange(); }
        }

        public ICommand EditarPagoCommand { private set; get; }
        public ICommand EliminarPagoCommand { private set; get; }
        public ICommand ListarPagosCommand { private set; get; }

        public INavigation Navigation { get; set; }

        async Task EditarPago(Type pageType)
        {
            if (_selectedPago != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = new PagoViewModel()
                {
                    Id_pago = SelectedPago.Id_pago,
                    Descripcion = SelectedPago.Descripcion,
                    Monto = SelectedPago.Monto,
                    Fecha = SelectedPago.Fecha,
                    Photo_recibo = SelectedPago.Photo_recibo
                };

                await Navigation.PushAsync(page);
            }
            else
            {
                UserDialogs.Instance.Alert(new AlertConfig
                {
                    Message = "Por favor seleccione un pago de la lista.",
                    OkText = "OK",
                    Title = "Alerta"
                });
            }
        }

        void EliminarPago()
        {
            if (SelectedPago != null)
            {
                var empEliminar = new Pagos()
                {
                    Id_pago = SelectedPago.Id_pago,
                    Descripcion = SelectedPago.Descripcion,
                    Monto = SelectedPago.Monto,
                    Fecha = SelectedPago.Fecha,
                    Photo_recibo = SelectedPago.Photo_recibo
                };

                var eliminar = App.BaseDatos.EliminarPago(empEliminar);

                UserDialogs.Instance.Alert(new AlertConfig
                {
                    Message = "Pago eliminado correctamente. \n\nPuede ver la lista actualizada presionando el botón de Listado Pagos.",
                    OkText = "OK",
                    Title = "Confirmación"
                });
                ListadoPagos();
                Navigation.PopAsync();
            }
            else
            {
                UserDialogs.Instance.Alert(new AlertConfig
                {
                    Message = "Por favor seleccione un pago de la lista.",
                    OkText = "OK",
                    Title = "Alerta"
                });
            }
        }
    }
}

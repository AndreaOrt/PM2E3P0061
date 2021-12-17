using System;
using System.Windows.Input;
using PM2E3P201820110061.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Plugin.Media;
using System.Diagnostics;
using Acr.UserDialogs;

namespace PM2E3P201820110061.ViewModels
{
    public class PagoViewModel: BaseViewModel
    {
        public PagoViewModel()
        {
            GuardarPagoCommand = new Command(() => GuardarPago());
            TomarFotoReciboCommand = new Command(() => TomarFotografiaRecibo());
            LimpiarCommand = new Command(() => Clear());
        }

        private int _id_pago;
        public int Id_pago
        {
            get { return _id_pago; }
            set { _id_pago = value; OnPropertyChange(); }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChange(); }
        }

        private double _monto;

        public double Monto
        {
            get { return _monto; }
            set { _monto = value; OnPropertyChange(); }
        }

        private double _fecha;

        public double Fecha
        {
            get { return _fecha; }
            set { _fecha = value; OnPropertyChange(); }
        }

        private Byte[] _photo_recibo;

        public Byte[] Photo_recibo
        {
            get { return _photo_recibo; }
            set { _photo_recibo = value; OnPropertyChange(); }
        }

        private ImageSource _imagen_recibo;

        public ImageSource Imagen_recibo
        {
            get { return _imagen_recibo; }
            set { _imagen_recibo = value; OnPropertyChange(); }
        }

        private string _pathfoto;

        public string pathFoto
        {
            get { return _pathfoto; }
            set { _pathfoto = value; OnPropertyChange(); }
        }


        public ICommand GuardarPagoCommand { set; get; }
        public ICommand TomarFotoReciboCommand { set; get; }
        public ICommand LimpiarCommand { private set; get; }

        void Clear()
        {
            Descripcion = string.Empty;
            Monto = 0;
            Fecha = 0;
        }

        void GuardarPago()
        {
            if (System.String.IsNullOrWhiteSpace(Descripcion))
            {
                UserDialogs.Instance.Alert(new AlertConfig
                {
                    Message = "Por favor agregue una descripción.",
                    OkText = "OK",
                    Title = "Alerta"
                });
                return;
            }
            else if (System.String.IsNullOrWhiteSpace(Convert.ToString(Monto)) || Monto == 0)
            {
                UserDialogs.Instance.Alert(new AlertConfig
                {
                    Message = "Por favor agregue el monto del pago.",
                    OkText = "OK",
                    Title = "Alerta"
                });
                return;
            }
            else if (System.String.IsNullOrWhiteSpace(Convert.ToString(Fecha)) || Fecha == 0)
            {
                UserDialogs.Instance.Alert(new AlertConfig
                {
                    Message = "Por favor agregue la fecha.",
                    OkText = "OK",
                    Title = "Alerta"
                });
                return;
            }
            else if (System.String.IsNullOrWhiteSpace(pathFoto))
            {
                if(Id_pago == 0)
                {
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = "Por favor agregue la foto del recibo.",
                        OkText = "OK",
                        Title = "Alerta"
                    });
                    return;
                }
            }
            else
            {
                Pagos pago = new Pagos()
                {
                    Id_pago = Id_pago,
                    Descripcion = Descripcion,
                    Monto = Monto,
                    Fecha = Fecha,
                    Photo_recibo = Photo_recibo
                };

                App.BaseDatos.GuardarPago(pago);
                Clear();

                if(Id_pago != 0)
                {
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = "Se ha modificado el pago correctamente. \n\nPuede ver la modificación presionando el botón de listado de Pagos.",
                        OkText = "OK",
                        Title = "Confirmación"
                    });
                }
                else
                {
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = "Pago creado correctamente. \n\nPuede verlo en el listado de Pagos.",
                        OkText = "OK",
                        Title = "Confirmación"
                    });
                }
            }
        }

        private async void TomarFotografiaRecibo()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                return;

            var fotoRecibo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions { SaveToAlbum = true });

            if (fotoRecibo == null)
                return;

            //convertir a arreglo de bytes
            byte[] fotoReciboByte = System.IO.File.ReadAllBytes(fotoRecibo.AlbumPath);

            Photo_recibo = fotoReciboByte;

            Debug.WriteLine($"PATH EN BYTE[]: {fotoReciboByte.ToString()}");

            string pathBase64 = Convert.ToBase64String(fotoReciboByte);

            pathFoto = pathBase64;
            Imagen_recibo = ImageSource.FromStream(() => { return fotoRecibo.GetStream();});

            UserDialogs.Instance.Alert(new AlertConfig
            {
                Message = "Se realizó el proceso de tomar la fotografia correctamente.",
                OkText = "OK",
                Title = "Confirmación"
            });
        }
    }
}

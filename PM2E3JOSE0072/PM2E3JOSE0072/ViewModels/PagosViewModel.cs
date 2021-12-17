using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using System.Collections.ObjectModel;
using PM2E3JOSE0072.DataBase;
using PM2E3JOSE0072.Models;
using Xamarin.Forms;




namespace PM2E3JOSE0072.ViewModels
{
    public class PagosViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }

        int idPagos;
        string descripcion;
        double monto;
        double fecha;        

        public int IdPagos { get => idPagos; set { SetProperty(ref idPagos, value); } }

        public string Descripcion { get => descripcion; set { SetProperty(ref descripcion, value); } }

        public double Monto { get => monto; set { SetProperty(ref monto, value); } }

        public double Fecha { get => fecha; set { SetProperty(ref fecha, value); } }
    

        Page Page;
        Pagos pago;

        public PagosViewModel(Page page, Pagos pagoo)
        {
            Page = page;

            pago = pagoo;

            LoadData();

            SaveCommand = new Command(async () => {
                if (Validate())
                {
                    CargarDatos();
                    UserDialogs.Instance.ShowLoading("Guardando");
                    int respuesta = await DataBase.DataBase.CurrentDB.SavePagos(pago);
                    if (respuesta == Constantes.SUCCESS)
                    {
                        Constantes.WasChange = true; //Variable bandera para determinar si se realizo un cambio
                        await Page.DisplayAlert("Información", "Guardado con éxito.", "Aceptar");
                        await Page.Navigation.PopAsync();
                    }
                    else
                        await Page.DisplayAlert("Información", "Error al guardar.", "Aceptar");
                    UserDialogs.Instance.HideLoading();
                }
                else
                    await Page.DisplayAlert("Advertensia", "Debe llenar todos los campos.", "Aceptar");
            });

        }

        void LoadData()
        {
            
            if (pago.IdPagos > 0)
            {
                Title = "Actualizar empleado";
                Descripcion = pago.Descripcion;
                Monto = pago.Monto;
                Fecha = pago.Fecha;               

            }
            else
            {
                Title = "Agregar empleado";                
            }
        }

        bool Validate()
        {
            if (!string.IsNullOrEmpty(Descripcion)
                )
                return true;
            else
                return false;
        }
        void CargarDatos()
        {
            pago.Descripcion = Descripcion;
            pago.Fecha = Fecha;
            pago.Monto = Monto;                        
        }

    }
}

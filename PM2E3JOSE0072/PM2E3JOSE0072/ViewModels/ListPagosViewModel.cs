using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using System.Collections.ObjectModel;
using PM2E3JOSE0072.DataBase;
using PM2E3JOSE0072.Models;
using PM2E3JOSE0072.Views;
using Xamarin.Forms;

namespace PM2E3JOSE0072.ViewModels
{
    class ListPagosViewModel : BaseViewModel
    {
        public Command DeleteCommand { get; }
        public Command EditCommand { get; }
        public Command AddCommand { get; }
        ObservableCollection<Pagos> listPagos;


        bool isEmpty;
        bool isNotEmpty;
        string foto;
        public bool IsEmpty { get => isEmpty; set { SetProperty(ref isEmpty, value); } }
        public bool IsNotEmpty { get => isNotEmpty; set { SetProperty(ref isNotEmpty, value); } }

        public ObservableCollection<Pagos> ListEmployees { get => listPagos; set { SetProperty(ref listPagos, value); } }

        public string Foto { get => foto; set { SetProperty(ref foto, value); } }

        Page Page;

        public ListPagosViewModel(Page page)
        {
            Page = page;
            Constantes.WasChange = true;//Se inicializa como true para que cargue los datos de la lista

            DeleteCommand = new Command(async (pagosSelected) =>
            {
                var pagoo = pagosSelected as Pagos;
                bool canDelete = await Page.DisplayAlert("Advertencia", "¿Seguro desea eliminar a " + pagoo.Descripcion + " " + pagoo.Fecha + "?", "Aceptar", "Cancelar");
                if (canDelete)
                {
                    int res = await DataBase.DataBase.CurrentDB.DeletePagos((Pagos)pagosSelected);
                    if (res == Constantes.SUCCESS)
                        ListEmployees.Remove((Pagos)pagosSelected);
                    Load();
                }

            });
            EditCommand = new Command(async (pagoSelected) =>
            {
                var pagoo = pagoSelected as Pagos;
                UserDialogs.Instance.ShowLoading("Cargando");
                await Page.Navigation.PushAsync(new PagoPage(pagoo));
                UserDialogs.Instance.HideLoading();
            });
            AddCommand = new Command(async () =>
            {
                await Page.Navigation.PushAsync(new PagoPage(new Pagos()));
            });
        }
        public async void Load()
        {
            Title = "Empleados";
            Foto = "lista_vacia.png";

            int count = await DataBase.DataBase.CurrentDB.GetPagosCount();
            if (count > 0)
            {
                if (Constantes.WasChange)//Cargara la lista solo cuanda hay sucesido un cambio
                {
                    var list = await DataBase.DataBase.CurrentDB.GetAllPagos();
                    ListEmployees = new ObservableCollection<Pagos>(list);
                    Constantes.WasChange = false;//Se resetean los cambios
                }
                IsEmpty = false;
                IsNotEmpty = !IsEmpty;
            }
            else
            {

                IsEmpty = true;
                IsNotEmpty = !IsEmpty;

            }

        }
    }
}

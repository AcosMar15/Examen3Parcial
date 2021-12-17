using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E3JOSE0072.ViewModels;

namespace PM2E3JOSE0072.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPagos : ContentPage
    {
        ListPagosViewModel PagosListViewModel;
        public ListPagos()
        {
            InitializeComponent();
            PagosListViewModel = new ListPagosViewModel(this);
            BindingContext = PagosListViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PagosListViewModel.Load();
        }
    }
}
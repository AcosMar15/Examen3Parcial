using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E3JOSE0072.Models;
using PM2E3JOSE0072.ViewModels;


namespace PM2E3JOSE0072.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoPage : ContentPage
    {
        public PagoPage(Pagos pago)
        {
            InitializeComponent();
            BindingContext = new PagosViewModel(this, pago);
        }
    }
}
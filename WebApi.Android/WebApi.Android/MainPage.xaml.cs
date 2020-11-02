using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WebApi.Android
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetProducts();
        }

        private async void GetProducts()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync("https://localhost:44347/api/Employee/GetEmployeeById/1");

            var products = JsonConvert.
        }
    }
}

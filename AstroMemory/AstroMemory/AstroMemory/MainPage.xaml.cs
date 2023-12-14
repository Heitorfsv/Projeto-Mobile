using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace AstroMemory
{
    public partial class MainPage : ContentPage
    {
        URL imagem = new URL();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = imagem;
        }

        public string API_key = "QQnn5JJ17EubXsHTv5bT03J1dfrLcuSDFUfdu1NX";
        private HttpClient _client = new HttpClient();

        public void clicado(object sender, EventArgs args)
        {
            Label1.Text = "Esta é sua imagem especial";
            DateTime selectedDate = puxar_data.Date;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");
            int ano = selectedDate.Year;
            int mes = selectedDate.Month;
            int dia = selectedDate.Day;

            if (ano < 1995)
            {
                if (mes < 6)
                {
                    if (dia < 16)
                    {
                        Label1.Text = "Selecione uma data a partir de 16 de junho de 1995";
                    }
                }
            }

            PuxarDados(API_key, formattedDate);
        }

        async Task PuxarDados(string key, string data)
        {
            try
            {
                string APIUrl = String.Format("https://api.nasa.gov/planetary/apod?api_key={0}&date={1}", key, data);
                HttpResponseMessage response = await _client.GetAsync(APIUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(json);
                    URL imagem = new URL();
                    imagem.url = jsonObject["url"].ToString();
                    BindingContext = imagem;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}



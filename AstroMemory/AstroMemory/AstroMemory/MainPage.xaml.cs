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
            DateTime selectedDate = puxar_data.Date;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");
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



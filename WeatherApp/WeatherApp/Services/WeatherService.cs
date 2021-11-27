using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        const string ApiKey = "a519d2565f58343b5f157d056e658aca"; 

        public async Task<WeatherInfo> GetCityWeather()
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync
                ($"https://api.openweathermap.org/data/2.5/weather?q=tallinn&units=metric&appid={ApiKey}");
                var data = JsonConvert.DeserializeObject<WeatherInfo>(response);
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
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
        const string ApiKey = "2818e263edb914f4a5ee5f410b9feff3";
        
        public async Task<WeatherInfo> GetCityWeather(string city)
        {
            var client = new HttpClient();

            try
            {

                var response = await client.GetStringAsync
               ($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={ApiKey}");
                var data = JsonConvert.DeserializeObject<WeatherInfo>(response);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<Byte[]>GetImageFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var msg = await client.GetAsync(url);
                if (msg.IsSuccessStatusCode)
                {
                    var byteArray = await msg.Content.ReadAsByteArrayAsync();
                    return byteArray;                }
            }
            return null;
        }
    }
}
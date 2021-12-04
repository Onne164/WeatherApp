using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using WeatherApp.Services;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var cityEditText = FindViewById<EditText>(Resource.Id.cityEditText);
            var searchButton = FindViewById<Button>(Resource.Id.searchButton);
            var temperatureTextView = FindViewById<TextView>(Resource.Id.TemperatureTextView);
            var windTextView = FindViewById<TextView>(Resource.Id.WindTextView);
            var weatherImageView = FindViewById<ImageView>(Resource.Id.weatherImageView); 
            var weatherService = new WeatherService();

            searchButton.Click += async delegate
            {
                var data = await weatherService.GetCityWeather(cityEditText.Text);
                temperatureTextView.Text = data.main.temp.ToString();
                windTextView.Text = data.wind.speed.ToString();

                var imageBytes = await weatherService.GetImageFromUrl($"https://openweathermap.org/img/wn/{data.weather[0].icon}@2x.png");
                var bitmap = await BitmapFactory.DecodeByteArrayAsync(imageBytes,0, imageBytes.Length);
                weatherImageView.SetImageBitmap(bitmap);
            };

          
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
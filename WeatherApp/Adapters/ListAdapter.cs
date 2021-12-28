using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Models;

namespace WeatherApp.Adapters
{
    public class ListAdapter : BaseAdapter<WeatherItem>
    {
        List<WeatherItem> _items;
        Activity _context;

        public ListAdapter(Activity context, List<WeatherItem> items)
        {
            _items = items;
            _context = context;
        }

        public override WeatherItem this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.forecast_row, null);
            view.FindViewById<TextView>(Resource.Id.forecastTimeTextView).Text = _items[position].dt_txt;
            view.FindViewById<TextView>(Resource.Id.forecastTempTextView).Text = _items[position].main.temp.ToString() + " °C";
            var ic = view.FindViewById<ImageView>(Resource.Id.forecastImageView);
            ic.SetImageBitmap((Android.Graphics.Bitmap)_items[position].weather[0].Image);
            //view.FindViewById<ImageView>(Resource.Id.forecastImageView).SetImageResource((int)_items[position].weather[0].Image);

            return view;
        }
    }
}
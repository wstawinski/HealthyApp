using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthyApp.Models;

namespace HealthyApp.Adapters
{
    class BloodConditionAdapter : BaseAdapter<BloodConditionMeasurement>
    {
        Activity context;
        List<BloodConditionMeasurement> items;

        public BloodConditionAdapter(Activity context, List<BloodConditionMeasurement> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override BloodConditionMeasurement this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.BloodConditionHistoryDataRow, null);
            }

            if (item.BloodSugarLevel > 100)
                convertView.FindViewById<TextView>(Resource.Id.textViewBloodConditionHistoryDataRowSugarLevel).SetTextColor(Android.Graphics.Color.Red);
            else
                convertView.FindViewById<TextView>(Resource.Id.textViewBloodConditionHistoryDataRowSugarLevel).SetTextColor(Android.Graphics.Color.Black);

            convertView.FindViewById<TextView>(Resource.Id.textViewBloodConditionHistoryDataRowDate).Text = item.MeasurementDate.ToString("d MMMM", CultureInfo.CreateSpecificCulture("pl"));
            convertView.FindViewById<TextView>(Resource.Id.textViewBloodConditionHistoryDataRowSugarLevel).Text = item.BloodSugarLevel.ToString();

            return convertView;
        }
    }
}
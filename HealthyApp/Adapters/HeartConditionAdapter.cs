﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthyApp.Models;

namespace HealthyApp.Adapters
{
    class HeartConditionAdapter : BaseAdapter<HeartConditionMeasurement>
    {
        Activity context;
        List<HeartConditionMeasurement> items;

        public HeartConditionAdapter(Activity context, List<HeartConditionMeasurement> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override HeartConditionMeasurement this[int position]
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

            if(convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.HeartConditionHistoryDataRow, null);
            }

            if (item.UpperBloodPressure > 140)
                convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowUpperBloodPressure).SetTextColor(Android.Graphics.Color.Red);
            else
                convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowUpperBloodPressure).SetTextColor(Android.Graphics.Color.Black);
            if (item.LowerBloodPressure > 90)
                convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowLowerBloodPressure).SetTextColor(Android.Graphics.Color.Red);
            else
                convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowLowerBloodPressure).SetTextColor(Android.Graphics.Color.Black);
            if (item.HeartRate < 60 || item.HeartRate > 100)
                convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowHeartRate).SetTextColor(Android.Graphics.Color.Red);
            else
                convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowHeartRate).SetTextColor(Android.Graphics.Color.Black);

            convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowDate).Text = item.MeasurementDate.ToString("d MMMM", CultureInfo.CreateSpecificCulture("pl"));
            convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowUpperBloodPressure).Text = item.UpperBloodPressure.ToString();
            convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowLowerBloodPressure).Text = item.LowerBloodPressure.ToString();
            convertView.FindViewById<TextView>(Resource.Id.textViewHeartConditionHistoryDataRowHeartRate).Text = item.HeartRate.ToString();
            
            return convertView;
        }
    }
}
﻿using System;
using Android.App;
using Android.OS;
using Android.Widget;
using HealthyApp.Models;
using HealthyApp.Services;

namespace HealthyApp
{
    [Activity(Label = "Ciśnienie krwi i tętno")]
    public class HeartConditionMainActivity : Activity
    {
        EditText editTextHeartConditionMainUpperPressure;
        EditText editTextHeartConditionMainLowerPressure;
        EditText editTextHeartConditionMainHeartRate;
        Button buttonHeartConditionMainSubmit;

        readonly HeartConditionMeasurementsService service = new HeartConditionMeasurementsService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HeartConditionMain);

            FindViews();
            HandleEvents();

        }

        private void FindViews()
        {
            editTextHeartConditionMainUpperPressure = FindViewById<EditText>(Resource.Id.editTextHeartConditionMainUpperPressure);
            editTextHeartConditionMainLowerPressure = FindViewById<EditText>(Resource.Id.editTextHeartConditionMainLowerPressure);
            editTextHeartConditionMainHeartRate = FindViewById<EditText>(Resource.Id.editTextHeartConditionMainHeartRate);
            buttonHeartConditionMainSubmit = FindViewById<Button>(Resource.Id.buttonHeartConditionMainSubmit);
        }

        private void HandleEvents()
        {
            buttonHeartConditionMainSubmit.Click += ButtonHeartConditionMainSubmit_Click;
        }

        private void ButtonHeartConditionMainSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editTextHeartConditionMainUpperPressure.Text) ||
                string.IsNullOrEmpty(editTextHeartConditionMainLowerPressure.Text) ||
                string.IsNullOrEmpty(editTextHeartConditionMainHeartRate.Text))
                return;

            var upperPressure = Int32.Parse(editTextHeartConditionMainUpperPressure.Text);
            var lowerPressure = Int32.Parse(editTextHeartConditionMainLowerPressure.Text);
            var hearRate = Int32.Parse(editTextHeartConditionMainHeartRate.Text);

            var measurement = new HeartConditionMeasurement()
            {
                UpperBloodPressure = upperPressure,
                LowerBloodPressure = lowerPressure,
                HeartRate = hearRate,
                MeasurementDate = DateTime.Now
            };
            service.SaveHeartConditionMeasurement(measurement);

            var confirmationDialog = new AlertDialog.Builder(this);
            confirmationDialog.SetTitle("Potwierdzenie");
            confirmationDialog.SetMessage("Twój pomiar został zapisany.");
            confirmationDialog.Show();
        }
    }
}
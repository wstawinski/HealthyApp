using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using HealthyApp.Models;
using HealthyApp.Services;

namespace HealthyApp
{
    [Activity(Label = "Poziom cukru we krwi")]
    public class BloodConditionMainActivity : Activity
    {
        EditText editTextBloodConditionMainSugarLevel;
        Button buttonBloodConditionMainSubmit;
        Button buttonBloodConditionMainHistory;

        readonly BloodConditionMeasurementsService service = new BloodConditionMeasurementsService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BloodConditionMain);

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            editTextBloodConditionMainSugarLevel = FindViewById<EditText>(Resource.Id.editTextBloodConditionMainSugarLevel);
            buttonBloodConditionMainSubmit = FindViewById<Button>(Resource.Id.buttonBloodConditionMainSubmit);
            buttonBloodConditionMainHistory = FindViewById<Button>(Resource.Id.buttonBloodConditionMainHistory);
        }

        private void HandleEvents()
        {
            buttonBloodConditionMainSubmit.Click += ButtonBloodConditionMainSubmit_Click;
            buttonBloodConditionMainHistory.Click += ButtonBloodConditionMainHistory_Click;
        }

        private void ButtonBloodConditionMainSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editTextBloodConditionMainSugarLevel.Text))
            {
                var alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Uwaga!");
                alertDialog.SetMessage("Podaj poziom cukru we krwi.");
                alertDialog.Show();
                return;
            }

            if (service.CheckIfMeasurementWasSavedToday())
            {
                var alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Uwaga!");
                alertDialog.SetMessage("Dzisiaj już wprowadziłeś wynik pomiaru.");
                alertDialog.Show();
                return;
            }

            var sugarLevel = Int32.Parse(editTextBloodConditionMainSugarLevel.Text);

            var measurement = new BloodConditionMeasurement()
            {
                BloodSugarLevel = sugarLevel,
                MeasurementDate = DateTime.Now
            };
            service.SaveBloodConditionMeasurement(measurement);

            var confirmationDialog = new AlertDialog.Builder(this);
            confirmationDialog.SetTitle("Potwierdzenie");
            confirmationDialog.SetMessage("Twój pomiar został zapisany.");
            confirmationDialog.Show();
        }

        private void ButtonBloodConditionMainHistory_Click(object sender, EventArgs e)
        {
            var measurements = service.GetAllBloodConditionMeasurements();
            if (measurements.Count == 0)
            {
                var alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Uwaga!");
                alertDialog.SetMessage("Nie zapisano jeszcze żadnego wyniku.");
                alertDialog.Show();
                return;
            }
            var intent = new Intent(this, typeof(BloodConditionHistoryActivity));
            StartActivity(intent);
        }
    }
}
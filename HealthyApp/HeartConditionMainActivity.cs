using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
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
        Button buttonHeartConditionMainHistory;

        readonly HeartConditionMeasurementsService service = new HeartConditionMeasurementsService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HeartConditionMain);

            FindViews();
            HandleEvents();
            
            InputMethodManager imm = GetSystemService(InputMethodService) as InputMethodManager;  
            imm.HideSoftInputFromWindow(editTextHeartConditionMainUpperPressure.WindowToken, HideSoftInputFlags.None);
        }

        private void FindViews()
        {
            editTextHeartConditionMainUpperPressure = FindViewById<EditText>(Resource.Id.editTextHeartConditionMainUpperPressure);
            editTextHeartConditionMainLowerPressure = FindViewById<EditText>(Resource.Id.editTextHeartConditionMainLowerPressure);
            editTextHeartConditionMainHeartRate = FindViewById<EditText>(Resource.Id.editTextHeartConditionMainHeartRate);
            buttonHeartConditionMainSubmit = FindViewById<Button>(Resource.Id.buttonHeartConditionMainSubmit);
            buttonHeartConditionMainHistory = FindViewById<Button>(Resource.Id.buttonHeartConditionMainHistory);
        }

        private void HandleEvents()
        {
            buttonHeartConditionMainSubmit.Click += ButtonHeartConditionMainSubmit_Click;
            buttonHeartConditionMainHistory.Click += ButtonHeartConditionMainHistory_Click;
        }

        private void ButtonHeartConditionMainSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editTextHeartConditionMainUpperPressure.Text) ||
                string.IsNullOrEmpty(editTextHeartConditionMainLowerPressure.Text) ||
                string.IsNullOrEmpty(editTextHeartConditionMainHeartRate.Text))
            {
                var alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Uwaga!");
                alertDialog.SetMessage("Aby zapisać wynik musisz uzupełnić wszystkie pola.");
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

        private void ButtonHeartConditionMainHistory_Click(object sender, EventArgs e)
        {
            var measurements = service.GetAllHeartConditionMeasurements();
            if (measurements.Count == 0)
            {
                var alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Uwaga!");
                alertDialog.SetMessage("Nie zapisano jeszcze żadnego wyniku.");
                alertDialog.Show();
                return;
            }
            var intent = new Intent(this, typeof(HeartConditionHistoryActivity));
            StartActivity(intent);
        }
    }
}
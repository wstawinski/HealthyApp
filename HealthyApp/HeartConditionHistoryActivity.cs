using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthyApp.Adapters;
using HealthyApp.Services;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;


namespace HealthyApp
{
    [Activity(Label = "Historia pomiarów")]
    public class HeartConditionHistoryActivity : Activity
    {
        Spinner spinnerHeartConditionHistory;
        ChartView chartViewHeartConditionHistoryUpperBloodPressure;
        ChartView chartViewHeartConditionHistoryLowerBloodPressure;
        ListView listViewHeartConditionBloodPressureHistory;

        readonly HeartConditionMeasurementsService service = new HeartConditionMeasurementsService();
        List<string> availableMonthsForSpinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HeartConditionHistory);

            FindViews();
            HandleEvents();

            availableMonthsForSpinner = service.GetAvailableMonthsForSpinner();
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, availableMonthsForSpinner);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerHeartConditionHistory.Adapter = adapter;

            var lastAvailableMonth = availableMonthsForSpinner[0];
            var measurementsInLastAvailableMonth = service.GetMeasurementsByMonth(lastAvailableMonth);

            var upperBloodPressureEntries = new List<Entry>();
            var lowerBloodPressureEntries = new List<Entry>();
            foreach (var measurement in measurementsInLastAvailableMonth)
            {
                upperBloodPressureEntries.Add(new Entry(measurement.UpperBloodPressure)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.UpperBloodPressure.ToString(),
                    Color = SKColor.Parse("#FF0000")
                });

                lowerBloodPressureEntries.Add(new Entry(measurement.LowerBloodPressure)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.LowerBloodPressure.ToString(),
                    Color = SKColor.Parse("#0040FF")
                });
            }
            var upperBloodPressureChart = new PointChart { Entries = upperBloodPressureEntries };
            var lowerBloodPressureChart = new PointChart { Entries = lowerBloodPressureEntries };
            chartViewHeartConditionHistoryUpperBloodPressure.Chart = upperBloodPressureChart;
            chartViewHeartConditionHistoryLowerBloodPressure.Chart = lowerBloodPressureChart;

            var bloodPressureAdapter = new HeartConditionBloodPressureAdapter(this, measurementsInLastAvailableMonth);
            listViewHeartConditionBloodPressureHistory.Adapter = bloodPressureAdapter;
        }

        private void FindViews()
        {
            spinnerHeartConditionHistory = FindViewById<Spinner>(Resource.Id.spinnerHeartConditionHistory);
            chartViewHeartConditionHistoryUpperBloodPressure = FindViewById<ChartView>(Resource.Id.chartViewHeartConditionHistoryUpperBloodPressure);
            chartViewHeartConditionHistoryLowerBloodPressure = FindViewById<ChartView>(Resource.Id.chartViewHeartConditionHistoryLowerBloodPressure);
            listViewHeartConditionBloodPressureHistory = FindViewById<ListView>(Resource.Id.listViewHeartConditionBloodPressureHistory);
        }

        private void HandleEvents()
        {
            spinnerHeartConditionHistory.ItemSelected += SpinnerHeartConditionHistory_ItemSelected;
        }

        private void SpinnerHeartConditionHistory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selectedMonth = availableMonthsForSpinner[e.Position];
            var measurementsInSelectedMonth = service.GetMeasurementsByMonth(selectedMonth);

            var upperBloodPressureEntries = new List<Entry>();
            var lowerBloodPressureEntries = new List<Entry>();
            foreach (var measurement in measurementsInSelectedMonth)
            {
                upperBloodPressureEntries.Add(new Entry(measurement.UpperBloodPressure)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.UpperBloodPressure.ToString(),
                    Color = SKColor.Parse("#FF0000")
                });

                lowerBloodPressureEntries.Add(new Entry(measurement.LowerBloodPressure)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.LowerBloodPressure.ToString(),
                    Color = SKColor.Parse("#0040FF")
                });
            }
            var upperBloodPressureChart = new PointChart { Entries = upperBloodPressureEntries };
            var lowerBloodPressureChart = new PointChart { Entries = lowerBloodPressureEntries };
            chartViewHeartConditionHistoryUpperBloodPressure.Chart = upperBloodPressureChart;
            chartViewHeartConditionHistoryLowerBloodPressure.Chart = lowerBloodPressureChart;

            var bloodPressureAdapter = new HeartConditionBloodPressureAdapter(this, measurementsInSelectedMonth);
            listViewHeartConditionBloodPressureHistory.Adapter = bloodPressureAdapter;
        }
    }
}
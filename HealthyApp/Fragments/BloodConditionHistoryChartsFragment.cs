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
using HealthyApp.Services;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace HealthyApp.Fragments
{
    class BloodConditionHistoryChartsFragment : Fragment
    {
        Spinner spinnerBloodConditionHistoryCharts;
        ChartView chartViewBloodConditionHistoryChartsSugarLevel;

        readonly BloodConditionMeasurementsService service = new BloodConditionMeasurementsService();
        List<string> availableMonthsForSpinner;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();
            HandleEvents();

            availableMonthsForSpinner = service.GetAvailableMonthsForSpinner();
            var adapter = new ArrayAdapter<string>(View.Context, Android.Resource.Layout.SimpleSpinnerItem, availableMonthsForSpinner);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerBloodConditionHistoryCharts.Adapter = adapter;

            var firstAvailableMonth = availableMonthsForSpinner[0];
            var measurementsInFirstAvailableMonth = service.GetMeasurementsByMonth(firstAvailableMonth);
            var sugarLevelEntries = new List<Entry>();
            foreach (var measurement in measurementsInFirstAvailableMonth)
            {
                var sugarLevelEntry = new Entry(measurement.BloodSugarLevel)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.BloodSugarLevel.ToString(),
                    Color = SKColor.Parse("#0040FF")
                };
                if (measurement.BloodSugarLevel > 100)
                {
                    sugarLevelEntry.Color = SKColor.Parse("#FF0000");
                }
                sugarLevelEntries.Add(sugarLevelEntry);
            }
            var sugarLevelChart = new LineChart { Entries = sugarLevelEntries, LineMode = LineMode.Straight };
            chartViewBloodConditionHistoryChartsSugarLevel.Chart = sugarLevelChart;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.BloodConditionHistoryCharts, container, false);
        }

        private void FindViews()
        {
            spinnerBloodConditionHistoryCharts = View.FindViewById<Spinner>(Resource.Id.spinnerBloodConditionHistoryCharts);
            chartViewBloodConditionHistoryChartsSugarLevel = View.FindViewById<ChartView>(Resource.Id.chartViewBloodConditionHistoryChartsSugarLevel);
        }

        private void HandleEvents()
        {
            spinnerBloodConditionHistoryCharts.ItemSelected += SpinnerBloodConditionHistory_ItemSelected;
        }

        private void SpinnerBloodConditionHistory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selectedMonth = availableMonthsForSpinner[e.Position];
            var measurementsInSelectedMonth = service.GetMeasurementsByMonth(selectedMonth);

            var sugarLevelEntries = new List<Entry>();
            foreach (var measurement in measurementsInSelectedMonth)
            {
                var sugarLevelEntry = new Entry(measurement.BloodSugarLevel)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.BloodSugarLevel.ToString(),
                    Color = SKColor.Parse("#0040FF")
                };
                if (measurement.BloodSugarLevel > 100)
                {
                    sugarLevelEntry.Color = SKColor.Parse("#FF0000");
                }
                sugarLevelEntries.Add(sugarLevelEntry);
            }
            var sugarLevelChart = new LineChart { Entries = sugarLevelEntries, LineMode = LineMode.Straight };

            chartViewBloodConditionHistoryChartsSugarLevel.Chart = sugarLevelChart;
        }
    }
}
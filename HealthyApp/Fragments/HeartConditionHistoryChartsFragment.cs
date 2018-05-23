using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using HealthyApp.Services;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace HealthyApp.Fragments
{
    public class HeartConditionHistoryChartsFragment : Fragment
    {
        Spinner spinnerHeartConditionHistoryCharts;
        ChartView chartViewHeartConditionHistoryChartsUpperBloodPressure;
        ChartView chartViewHeartConditionHistoryChartsLowerBloodPressure;
        ChartView chartViewHeartConditionHistoryChartsHeartRate;

        readonly HeartConditionMeasurementsService service = new HeartConditionMeasurementsService();
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
            spinnerHeartConditionHistoryCharts.Adapter = adapter;

            var firstAvailableMonth = availableMonthsForSpinner[0];
            var measurementsInFirstAvailableMonth = service.GetMeasurementsByMonth(firstAvailableMonth);
            var upperBloodPressureEntries = new List<Entry>();
            var lowerBloodPressureEntries = new List<Entry>();
            var heartRateEntries = new List<Entry>();
            foreach (var measurement in measurementsInFirstAvailableMonth)
            {
                var upperBloodPressureEntry = new Entry(measurement.UpperBloodPressure)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.UpperBloodPressure.ToString(),
                    Color = SKColor.Parse("#0040FF")
                };
                if (measurement.UpperBloodPressure > 140)
                {
                    upperBloodPressureEntry.Color = SKColor.Parse("#FF0000");
                }
                upperBloodPressureEntries.Add(upperBloodPressureEntry);

                var lowerBloodPressureEntry = new Entry(measurement.LowerBloodPressure)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.LowerBloodPressure.ToString(),
                    Color = SKColor.Parse("#0040FF")
                };
                if (measurement.LowerBloodPressure > 90)
                {
                    lowerBloodPressureEntry.Color = SKColor.Parse("#FF0000");
                }
                lowerBloodPressureEntries.Add(lowerBloodPressureEntry);

                var heartRateEntry = new Entry(measurement.HeartRate)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.HeartRate.ToString(),
                    Color = SKColor.Parse("#0040FF")
                };
                if (measurement.HeartRate < 60 || measurement.HeartRate > 100)
                {
                    heartRateEntry.Color = SKColor.Parse("#FF0000");
                }
                heartRateEntries.Add(heartRateEntry);
            }
            var upperBloodPressureChart = new LineChart { Entries = upperBloodPressureEntries, LineMode = LineMode.Straight };
            var lowerBloodPressureChart = new LineChart { Entries = lowerBloodPressureEntries, LineMode = LineMode.Straight };
            var heartRateChart = new LineChart { Entries = heartRateEntries, LineMode = LineMode.Straight };
            chartViewHeartConditionHistoryChartsUpperBloodPressure.Chart = upperBloodPressureChart;
            chartViewHeartConditionHistoryChartsLowerBloodPressure.Chart = lowerBloodPressureChart;
            chartViewHeartConditionHistoryChartsHeartRate.Chart = heartRateChart;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.HeartConditionHistoryCharts, container, false);
        }

        private void FindViews()
        {
            spinnerHeartConditionHistoryCharts = View.FindViewById<Spinner>(Resource.Id.spinnerHeartConditionHistoryCharts);
            chartViewHeartConditionHistoryChartsUpperBloodPressure = View.FindViewById<ChartView>(Resource.Id.chartViewHeartConditionHistoryChartsUpperBloodPressure);
            chartViewHeartConditionHistoryChartsLowerBloodPressure = View.FindViewById<ChartView>(Resource.Id.chartViewHeartConditionHistoryChartsLowerBloodPressure);
            chartViewHeartConditionHistoryChartsHeartRate = View.FindViewById<ChartView>(Resource.Id.chartViewHeartConditionHistoryChartsHeartRate);
        }

        private void HandleEvents()
        {
            spinnerHeartConditionHistoryCharts.ItemSelected += SpinnerHeartConditionHistory_ItemSelected;
        }

        private void SpinnerHeartConditionHistory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selectedMonth = availableMonthsForSpinner[e.Position];
            var measurementsInSelectedMonth = service.GetMeasurementsByMonth(selectedMonth);

            var upperBloodPressureEntries = new List<Entry>();
            var lowerBloodPressureEntries = new List<Entry>();
            var heartRateEntries = new List<Entry>();
            foreach (var measurement in measurementsInSelectedMonth)
            {
                var upperBloodPressureEntry = new Entry(measurement.UpperBloodPressure)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.UpperBloodPressure.ToString(),
                    Color = SKColor.Parse("#0040FF")
                };
                if(measurement.UpperBloodPressure > 140)
                {
                    upperBloodPressureEntry.Color = SKColor.Parse("#FF0000");
                }
                upperBloodPressureEntries.Add(upperBloodPressureEntry);

                var lowerBloodPressureEntry = new Entry(measurement.LowerBloodPressure)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.LowerBloodPressure.ToString(),
                    Color = SKColor.Parse("#0040FF")
                };
                if (measurement.LowerBloodPressure > 90)
                {
                    lowerBloodPressureEntry.Color = SKColor.Parse("#FF0000");
                }
                lowerBloodPressureEntries.Add(lowerBloodPressureEntry);

                var heartRateEntry = new Entry(measurement.HeartRate)
                {
                    Label = measurement.MeasurementDate.Day.ToString(),
                    ValueLabel = measurement.HeartRate.ToString(),
                    Color = SKColor.Parse("#0040FF")
                };
                if (measurement.HeartRate < 60 || measurement.HeartRate > 100)
                {
                    heartRateEntry.Color = SKColor.Parse("#FF0000");
                }
                heartRateEntries.Add(heartRateEntry);
            }
            var upperBloodPressureChart = new LineChart { Entries = upperBloodPressureEntries, LineMode = LineMode.Straight };
            var lowerBloodPressureChart = new LineChart { Entries = lowerBloodPressureEntries, LineMode = LineMode.Straight };
            var heartRateChart = new LineChart { Entries = heartRateEntries, LineMode = LineMode.Straight };

            chartViewHeartConditionHistoryChartsUpperBloodPressure.Chart = upperBloodPressureChart;
            chartViewHeartConditionHistoryChartsLowerBloodPressure.Chart = lowerBloodPressureChart;
            chartViewHeartConditionHistoryChartsHeartRate.Chart = heartRateChart;
        }
    }
}
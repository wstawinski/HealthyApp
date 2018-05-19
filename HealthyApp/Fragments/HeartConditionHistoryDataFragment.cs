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
using HealthyApp.Adapters;
using HealthyApp.Services;

namespace HealthyApp.Fragments
{
    public class HeartConditionHistoryDataFragment : Fragment
    {
        Spinner spinnerHeartConditionHistoryData;
        ListView listViewHeartConditionHistory;

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
            var adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, availableMonthsForSpinner);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerHeartConditionHistoryData.Adapter = adapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.HeartConditionHistoryData, container, false);
        }

        private void FindViews()
        {
            spinnerHeartConditionHistoryData = View.FindViewById<Spinner>(Resource.Id.spinnerHeartConditionHistoryData);
            listViewHeartConditionHistory = View.FindViewById<ListView>(Resource.Id.listViewHeartConditionHistory);
        }

        private void HandleEvents()
        {
            spinnerHeartConditionHistoryData.ItemSelected += SpinnerHeartConditionHistory_ItemSelected;
        }

        private void SpinnerHeartConditionHistory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selectedMonth = availableMonthsForSpinner[e.Position];
            var measurementsInSelectedMonth = service.GetMeasurementsByMonth(selectedMonth);

            var heartConditionAdapter = new HeartConditionAdapter(Activity, measurementsInSelectedMonth);
            listViewHeartConditionHistory.Adapter = heartConditionAdapter;
        }
    }
}
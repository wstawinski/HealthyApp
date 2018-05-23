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

namespace HealthyApp.Fragments
{
    class BloodConditionHistoryDataFragment : Fragment
    {
        Spinner spinnerBloodConditionHistoryData;
        ListView listViewBloodConditionHistory;

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
            var adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, availableMonthsForSpinner);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerBloodConditionHistoryData.Adapter = adapter;

            var firstAvailableMonth = availableMonthsForSpinner[0];
            var measurementsInFirstAvailableMonth = service.GetMeasurementsByMonth(firstAvailableMonth);
            var bloodConditionAdapter = new BloodConditionAdapter(Activity, measurementsInFirstAvailableMonth.AsEnumerable().Reverse().ToList());
            listViewBloodConditionHistory.Adapter = bloodConditionAdapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.BloodConditionHistoryData, container, false);
        }

        private void FindViews()
        {
            spinnerBloodConditionHistoryData = View.FindViewById<Spinner>(Resource.Id.spinnerBloodConditionHistoryData);
            listViewBloodConditionHistory = View.FindViewById<ListView>(Resource.Id.listViewBloodConditionHistory);
        }

        private void HandleEvents()
        {
            spinnerBloodConditionHistoryData.ItemSelected += SpinnerBloodConditionHistory_ItemSelected;
        }

        private void SpinnerBloodConditionHistory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selectedMonth = availableMonthsForSpinner[e.Position];
            var measurementsInSelectedMonth = service.GetMeasurementsByMonth(selectedMonth);

            var BloodConditionAdapter = new BloodConditionAdapter(Activity, measurementsInSelectedMonth.AsEnumerable().Reverse().ToList());
            listViewBloodConditionHistory.Adapter = BloodConditionAdapter;
        }
    }
}
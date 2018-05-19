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
using HealthyApp.Fragments;
using HealthyApp.Services;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;


namespace HealthyApp
{
    [Activity(Label = "Historia wyników")]
    public class HeartConditionHistoryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HeartConditionHistory);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("Lista", new HeartConditionHistoryDataFragment());
            AddTab("Wykresy", new HeartConditionHistoryChartsFragment());
        }

        private void AddTab(string tabLabel, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabLabel);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = FragmentManager.FindFragmentById(Resource.Id.fragmentContainerHeartConditionHistory);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainerHeartConditionHistory, view);
            };

            ActionBar.AddTab(tab);
        }
    }
}
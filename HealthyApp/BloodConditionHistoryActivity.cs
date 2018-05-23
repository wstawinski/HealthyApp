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
using HealthyApp.Fragments;

namespace HealthyApp
{
    [Activity(Label = "Historia wyników")]
    public class BloodConditionHistoryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BloodConditionHistory);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("Lista", new BloodConditionHistoryDataFragment());
            AddTab("Wykresy", new BloodConditionHistoryChartsFragment());
        }

        private void AddTab(string tabLabel, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabLabel);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = FragmentManager.FindFragmentById(Resource.Id.fragmentContainerBloodConditionHistory);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainerBloodConditionHistory, view);
            };

            ActionBar.AddTab(tab);
        }
    }
}
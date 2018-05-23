using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using HealthyApp.Models;
using HealthyApp.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace HealthyApp
{
    [Activity(Label = "Zdrówko", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button buttonHeart;
        Button buttonBlood;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            ConfigureDatabase();

            FindViews();
            HandleEvents();
        }

        void ConfigureDatabase()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Zdrowko-BazaDanych.db3");
            var db = new SQLiteConnection(dbPath);

            db.CreateTable<BloodConditionMeasurement>();
            db.CreateTable<HeartConditionMeasurement>();
        }

        void FindViews()
        {
            buttonHeart = FindViewById<Button>(Resource.Id.buttonMainHeartCondition);
            buttonBlood = FindViewById<Button>(Resource.Id.buttonMainBloodCondition);
        }

        void HandleEvents()
        {
            buttonHeart.Click += ButtonHeart_Click;
            buttonBlood.Click += ButtonBlood_Click;
        }

        private void ButtonHeart_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(HeartConditionMainActivity));
            StartActivity(intent);
        }

        private void ButtonBlood_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(BloodConditionMainActivity));
            StartActivity(intent);
        }        
    }
}


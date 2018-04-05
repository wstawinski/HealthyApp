using Android.App;
using Android.OS;
using Android.Widget;
using HealthyApp.Models;
using HealthyApp.Repositories;
using SQLite;
using System;
using System.IO;
using System.Linq;

namespace HealthyApp
{
    [Activity(Label = "Zdrówko", MainLauncher = true)]
    public class MainActivity : Activity
    {        
        static readonly HeartConditionMeasurementsRepository heartConditionMeasurementsRepository = new HeartConditionMeasurementsRepository();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            ConfigureDatabase();

            var heartConditionMeasurement = new HeartConditionMeasurement()
            {
                UpperBloodPressure = 200,
                LowerBloodPressure = 80,
                HeartRate = 60,
                MeasurementDate = DateTime.Now
            };
            heartConditionMeasurementsRepository.SaveHeartConditionMeasurement(heartConditionMeasurement);

            TextView dbContent = FindViewById<TextView>(Resource.Id.textViewMainDbTest);
            var measurement = heartConditionMeasurementsRepository.GetAllHeartConditionMeasurements()[1];

            dbContent.Text = String.Format("Górne ciśnienie: {0}, Dolne ciśnienie: {1}, Tętno: {2}", measurement.UpperBloodPressure, measurement.LowerBloodPressure, measurement.HeartRate);

        }

        void ConfigureDatabase()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Zdrowko-BazaDanych.db3");
            var db = new SQLiteConnection(dbPath);

            db.CreateTable<BloodConditionMeasurement>();
            db.CreateTable<HeartConditionMeasurement>();
        }
    }
}


using HealthyApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HealthyApp.Repositories
{
    public class HeartConditionMeasurementsRepository
    {
        static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Zdrowko-BazaDanych.db3");
        static SQLiteConnection db;

        public HeartConditionMeasurementsRepository()
        {
            db = new SQLiteConnection(dbPath);
        }

        public List<HeartConditionMeasurement> GetAllHeartConditionMeasurements()
        {
            return db.Table<HeartConditionMeasurement>().ToList();
        }

        public void SaveHeartConditionMeasurement(HeartConditionMeasurement measurement)
        {
            db.Insert(measurement);
        }

    }
}

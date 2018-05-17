using HealthyApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HealthyApp.Repositories
{
    class HeartConditionMeasurementsRepository
    {
        readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Zdrowko-BazaDanych.db3");
        SQLiteConnection db;

        public HeartConditionMeasurementsRepository()
        {
            db = new SQLiteConnection(dbPath);
        }

        public void SaveHeartConditionMeasurement(HeartConditionMeasurement measurement)
        {
            db.Insert(measurement);
        }

        public List<HeartConditionMeasurement> GetAllHeartConditionMeasurements()
        {
            return db.Table<HeartConditionMeasurement>().ToList();
        }

        public List<DateTime> GetAvailableMonthsForSpinner()
        {
            var searchResults = from measurement in db.Table<HeartConditionMeasurement>()
                                group measurement by new { month = measurement.MeasurementDate.Month, year = measurement.MeasurementDate.Year } into grouped
                                select new DateTime(grouped.Key.year, grouped.Key.month, 1);
            return searchResults.Reverse().ToList();
        }

        public List<HeartConditionMeasurement> GetMeasurementsByMonth(DateTime month)
        {
            var measurements = from measurement in db.Table<HeartConditionMeasurement>().ToList()
                               where measurement.MeasurementDate.Year == month.Year && measurement.MeasurementDate.Month == month.Month
                               select measurement;
            return measurements.ToList();
        }
    }
}

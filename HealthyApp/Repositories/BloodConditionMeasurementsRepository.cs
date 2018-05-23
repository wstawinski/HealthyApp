using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HealthyApp.Models;
using SQLite;

namespace HealthyApp.Repositories
{
    class BloodConditionMeasurementsRepository
    {
        readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Zdrowko-BazaDanych.db3");
        SQLiteConnection db;

        public BloodConditionMeasurementsRepository()
        {
            db = new SQLiteConnection(dbPath);
        }

        public void SaveBloodConditionMeasurement(BloodConditionMeasurement measurement)
        {
            db.Insert(measurement);
        }

        public List<BloodConditionMeasurement> GetAllBloodConditionMeasurements()
        {
            return db.Table<BloodConditionMeasurement>().ToList();
        }

        public List<DateTime> GetAvailableMonthsForSpinner()
        {
            var searchResults = from measurement in db.Table<BloodConditionMeasurement>()
                                group measurement by new { month = measurement.MeasurementDate.Month, year = measurement.MeasurementDate.Year } into grouped
                                select new DateTime(grouped.Key.year, grouped.Key.month, 1);
            return searchResults.Reverse().ToList();
        }

        public List<BloodConditionMeasurement> GetMeasurementsByMonth(DateTime month)
        {
            var measurements = from measurement in db.Table<BloodConditionMeasurement>().ToList()
                               where measurement.MeasurementDate.Year == month.Year && measurement.MeasurementDate.Month == month.Month
                               select measurement;
            return measurements.ToList();
        }

        internal bool CheckIfMeasurementWasSavedToday()
        {
            var today = DateTime.Now.Date;

            var measurement = db.Table<BloodConditionMeasurement>().ToList()
                .Where(m => m.MeasurementDate.Year == today.Year && m.MeasurementDate.Month == today.Month && m.MeasurementDate.Day == today.Day)
                .FirstOrDefault();

            if (measurement != null)
                return true;
            return false;
        }
    }
}
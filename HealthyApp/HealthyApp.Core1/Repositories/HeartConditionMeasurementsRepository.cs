using HealthyApp.Core.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HealthyApp.Core1.Repositories
{
    public class HeartConditionMeasurementsRepository
    {
        static string dbPath;
        static SQLiteConnection db;

        public HeartConditionMeasurementsRepository(string _dbPath)
        {
            dbPath = _dbPath;
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

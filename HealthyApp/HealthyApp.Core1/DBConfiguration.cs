using HealthyApp.Core.Models;
using SQLite;
using System;
using System.IO;

namespace HealthyApp.Core1
{
    public static class DBConfiguration
    {
        static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Zdrowko-BazaDanych.db3");
        static readonly SQLiteConnection db = new SQLiteConnection(dbPath);

        public static void Configure()
        {
            db.CreateTable<BloodConditionMeasurement>();
            db.CreateTable<HeartConditionMeasurement>();
        }
   
    }
}

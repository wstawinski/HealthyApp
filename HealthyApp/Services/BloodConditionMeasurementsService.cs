using HealthyApp.Models;
using HealthyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HealthyApp.Services
{
    class BloodConditionMeasurementsService
    {
        readonly BloodConditionMeasurementsRepository repository = new BloodConditionMeasurementsRepository();

        public void SaveBloodConditionMeasurement(BloodConditionMeasurement measurement)
        {
            repository.SaveBloodConditionMeasurement(measurement);
        }

        public List<BloodConditionMeasurement> GetAllBloodConditionMeasurements()
        {
            return repository.GetAllBloodConditionMeasurements();
        }

        public List<string> GetAvailableMonthsForSpinner()
        {
            var searchResults = repository.GetAvailableMonthsForSpinner();

            var monthsForSpinner = new List<string>();
            foreach (var result in searchResults)
            {
                monthsForSpinner.Add(result.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("pl")));
            }

            return monthsForSpinner;
        }

        public List<BloodConditionMeasurement> GetMeasurementsByMonth(string selectedMonth)
        {
            var month = DateTime.ParseExact(selectedMonth, "MMMM yyyy", CultureInfo.CreateSpecificCulture("pl"));

            return repository.GetMeasurementsByMonth(month);
        }

        public bool CheckIfMeasurementWasSavedToday()
        {
            return repository.CheckIfMeasurementWasSavedToday();
        }
    }
}
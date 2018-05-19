using HealthyApp.Models;
using HealthyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HealthyApp.Services
{
    class HeartConditionMeasurementsService
    {
        readonly HeartConditionMeasurementsRepository repository = new HeartConditionMeasurementsRepository();

        public void SaveHeartConditionMeasurement(HeartConditionMeasurement measurement)
        {
            repository.SaveHeartConditionMeasurement(measurement);
        }

        public List<HeartConditionMeasurement> GetAllHeartConditionMeasurements()
        {
            return repository.GetAllHeartConditionMeasurements();
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

        public List<HeartConditionMeasurement> GetMeasurementsByMonth(string selectedMonth)
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
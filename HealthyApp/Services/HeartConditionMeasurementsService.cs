using HealthyApp.Models;
using HealthyApp.Repositories;
using System.Collections.Generic;

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
    }
}
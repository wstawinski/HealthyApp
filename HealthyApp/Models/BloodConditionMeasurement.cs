using System;
using SQLite;

namespace HealthyApp.Models
{
    [Table("BloodConditionMeasurements")]
    class BloodConditionMeasurement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int BloodSugarLevel { get; set; }

        [NotNull]
        public DateTime MeasurementDate { get; set; }

    }
}

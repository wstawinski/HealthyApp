using System;
using SQLite;

namespace HealthyApp.Core.Models
{
    [Table("BloodConditionMeasurements")]
    public class BloodConditionMeasurement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int BloodSugarLevel { get; set; }

        [NotNull]
        public DateTime MeasurementDate { get; set; }

    }
}

using System;
using SQLite;

namespace HealthyApp.Models
{
    [Table("HeartConditionMeasurements")]
    public class HeartConditionMeasurement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int UpperBloodPressure { get; set; }

        [NotNull]
        public int LowerBloodPressure { get; set; }

        [NotNull]
        public int HeartRate { get; set; }

        [NotNull]
        public DateTime MeasurementDate { get; set; }

    }
}

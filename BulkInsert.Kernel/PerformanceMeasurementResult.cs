using System;

namespace BulkInsert.Kernel
{
    public class PerformanceMeasurementResult
    {
        // ReSharper disable once NotAccessedField.Local
        // TODO To either use or remove _id
        private readonly Guid _id;
        public readonly bool _isTimeMeasured;
        public readonly string _measuredObjectName;
        public readonly string _noMeasurementReason;

        public int SampleSize { get; }
        public TimeSpan Value { get; }

        public PerformanceMeasurementResult(string measuredObjectName, int sampleSize, TimeSpan value) 
            : this(measuredObjectName, sampleSize, true)
        {
            Value = value;
        }

        public PerformanceMeasurementResult(string measuredObjectName, int sampleSize, string noMeasurementReason) 
            : this(measuredObjectName, sampleSize, false)
        {
            _noMeasurementReason = noMeasurementReason;
        }

        private PerformanceMeasurementResult(string measuredObjectName, int sampleSize, bool isTimeMeasured)
        {
            _id = Guid.NewGuid();
            _measuredObjectName = measuredObjectName;
            SampleSize = sampleSize;
            _isTimeMeasured = isTimeMeasured;
        }
    }
}

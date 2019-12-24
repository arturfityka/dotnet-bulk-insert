using System;

namespace BulkInsert.Kernel
{
    internal class PerformanceMeasurementResult
    {
        private readonly Guid _id;
        private readonly bool _isTimeMeasured;
        private readonly string _measuredObjectName;
        private readonly string _noMeasurementReason;

        internal int SampleSize { get; }
        internal TimeSpan Value { get; }

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

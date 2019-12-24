using System;

namespace BulkInsert.Kernel
{
    internal class PerformanceMeasurementResult
    {
        private readonly Guid _id;
        private readonly bool _isTimeMeasured;
        private readonly string _measuredImplementation;
        private readonly string _noMeasurementReason;

        internal int SampleSize { get; }
        internal TimeSpan Value { get; }

        public PerformanceMeasurementResult(string measuredImplementation, int sampleSize, TimeSpan value) 
            : this(measuredImplementation, sampleSize, true)
        {
            Value = value;
        }

        public PerformanceMeasurementResult(string measuredImplementation, int sampleSize, string noMeasurementReason) 
            : this(measuredImplementation, sampleSize, false)
        {
            _noMeasurementReason = noMeasurementReason;
        }

        private PerformanceMeasurementResult(string measuredImplementation, int sampleSize, bool isTimeMeasured)
        {
            _id = Guid.NewGuid();
            _measuredImplementation = measuredImplementation;
            SampleSize = sampleSize;
            _isTimeMeasured = isTimeMeasured;
        }
    }
}

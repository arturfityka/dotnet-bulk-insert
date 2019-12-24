using System;

namespace BulkInsert.Kernel
{
    public class PerformanceMeasurementResult
    {
        private readonly Guid _id;
        private readonly bool _isTimeMeasured;
        private readonly string _measuredImplementation;
        private readonly string _noMeasurementReason;
        private readonly int _sampleSize;
        private readonly TimeSpan _value;

        public PerformanceMeasurementResult(string measuredImplementation, int sampleSize, TimeSpan value) 
            : this(measuredImplementation, sampleSize, true)
        {
            _value = value;
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
            _sampleSize = sampleSize;
            _isTimeMeasured = isTimeMeasured;
        }
    }
}

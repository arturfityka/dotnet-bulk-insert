using BulkInsert.Kernel;
using Newtonsoft.Json;

namespace BulkInsert.Application.DTOs 
{
    public class PerformanceMeasurementResultDto
    {
        [JsonProperty]
        public int SampleSize { get; }
        [JsonProperty]
        public string Value { get; }
        [JsonProperty]
        public string ObjectUnderMeasurementName { get; }
        
        public PerformanceMeasurementResultDto(PerformanceMeasurementResult result)
        {
            SampleSize = result.SampleSize;
            Value = result._isTimeMeasured ? result.Value.ToString("c") : result._noMeasurementReason;
            ObjectUnderMeasurementName = result._measuredObjectName;
        }
    }
}
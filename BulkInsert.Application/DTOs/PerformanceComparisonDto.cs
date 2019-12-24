using System.Collections.Generic;
using System.Linq;
using BulkInsert.Kernel;
using Newtonsoft.Json;

namespace BulkInsert.Application.DTOs
{
    public class PerformanceComparisonDto
    {
        [JsonProperty]
        public IEnumerable<PerformanceMeasurementResultDto> MeasurementResults { get; }
        
        public PerformanceComparisonDto(IEnumerable<PerformanceMeasurementResult> measurementResults)
        {
            MeasurementResults = measurementResults.Select(result => new PerformanceMeasurementResultDto(result));
        }
    }
}

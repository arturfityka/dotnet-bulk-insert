using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BulkInsert.Kernel.Repositories;

namespace BulkInsert.Kernel 
{
  public class PerformanceMeasurementFacility
  {
    private readonly TimeSpan _poorPerformanceBoundary = TimeSpan.FromMilliseconds(100);
    private readonly IPaymentRepository _repository;
    private readonly IDictionary<int, TimeSpan> _results = new Dictionary<int, TimeSpan>();
        
    public PerformanceMeasurementFacility(IPaymentRepository repository)
    {
      _repository = repository;
    }

    public async Task MeasureAsync(Payment[] payments)
    {
      if (IsPoorPerformanceExpected(payments.Length))
      {
        return; // TODO add result with info about poor performance
      }
            
      var stopwatch = new Stopwatch();
      stopwatch.Start();

      await _repository.AddAsync(payments);
            
      stopwatch.Stop();

      await _repository.ClearAsync();
            
      _results.Add(payments.Length, stopwatch.Elapsed);
    }

    private bool IsPoorPerformanceExpected(int sampleSize)
    {
      if (_results.Count == 0)
      {
        return false;
      }

      return _results
       .Where(x => x.Key <= sampleSize)
       .Any(x => x.Value > _poorPerformanceBoundary);
    }
  }
}
namespace Computerized_maintenance_Logic_layer.Services
{

    /// <summary>
    /// use SRP for calculate 
    /// </summary>
    public sealed class CalculateService
    {
        public Single CalculateDownTimePerHour(DateTime StartEvent, DateTime? EndEvent)
        {
            if (!EndEvent.HasValue) return 0;

            if (StartEvent.CompareTo(EndEvent) > 0) throw new ArgumentException("The Start Envet is Greater than the End Event");


            var span = EndEvent?.Subtract(StartEvent).TotalHours;

            return Single.Parse(span.ToString()!);
        }


        //public Single CalculateMeanTimeBetweenFailure(byte[] OperationTime,int NumberofFiluer )
        //{
        //    if (LastFailureDate.CompareTo(CurrentFailureDate) > 0) throw new ArgumentException("The Last Failure Date is Greater than the Current Failure Date");
        //    span span = Span<byte>.Empty;
        //    return Single.Parse(span.ToString()!);
        //}

    }
}

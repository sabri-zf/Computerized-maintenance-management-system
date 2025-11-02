namespace Computerized_maintenance_Logic_layer.Services
{

    /// <summary>
    /// use SRP for calculate 
    /// </summary>
    public class CalculateService
    {
        public Single CalculateDownTimePerHour(DateTime StartEvent, DateTime EndEvent)
        {
            if (StartEvent.CompareTo(EndEvent) > 0) throw new ArgumentException("The Start Envet is Greater than the End Event");

            var span =  StartEvent.Subtract(EndEvent).Hours;

            return Single.Parse(span.ToString());
        }

    }
}

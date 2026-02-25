namespace Computerized_maintenance_Logic_layer.Services
{
    public sealed class LeapYearService
    {
        public short ProvideYear(bool IsLeapYear)
        {
            return (short)(IsLeapYear ? 366 : 365);
        }
    }
}

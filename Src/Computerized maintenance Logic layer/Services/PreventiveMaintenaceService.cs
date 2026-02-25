using computrized_maintenance_Data_Access.Enumes;

namespace Computerized_maintenance_Logic_layer.Services
{
    public sealed class PreventiveMaintenaceService(LeapYearService _service)
    {

        public float ConvertFequencyScheduleFromStringToDigit(En_FrequencyTask fequencySchedule)
        {

            var IsLeap = DateTime.IsLeapYear(DateTime.Now.Year);

            switch ((En_FrequencyTask)fequencySchedule)
            {
                case En_FrequencyTask.Daily:
                    return 1;
                case En_FrequencyTask.Weekly:
                    return 7;
                case En_FrequencyTask.Monthly:
                    return 30;
                case En_FrequencyTask.Quarterly:
                    return _service.ProvideYear(IsLeap) / 4;
                case En_FrequencyTask.SemiAnnual:
                    return _service.ProvideYear(IsLeap) / 2;
                case En_FrequencyTask.Annual:
                    return _service.ProvideYear(IsLeap);
                default:
                    return -1;
            }
        }
    }
}

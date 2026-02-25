using Computerized_maintenance_Logic_layer.Module.DownTimeTracking;
using Computerized_maintenance_Logic_layer.Module.preventiveMaintenanceManagement;

namespace CMMS_Api.Extensions
{
    internal static class DowntineAndPriventiveServiceCollectionExtension
    {


        public static IServiceCollection AddDownTimeAndPriventive(this IServiceCollection Services)
        {

            Services.AddScoped<ClsDownTimeEvents>();
            Services.AddScoped<ClspreventiveMaintenances>();
           

            return Services;
        }
    }

}

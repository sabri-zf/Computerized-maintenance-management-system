using Computerized_maintenance_Logic_layer.Module.workOrderManagement;

namespace CMMS_Api.Extensions
{
    internal static class WorkOrderServiceCollectionExtension
    {


        public static IServiceCollection AddWorkOrders(this IServiceCollection Services)
        {
            Services.AddScoped<ClsWorkorders>();
            Services.AddScoped<ClsWorkOrderParts>();
            Services.AddScoped<ClsWorkOrderHistory>();

            return Services;
        }

    }
}

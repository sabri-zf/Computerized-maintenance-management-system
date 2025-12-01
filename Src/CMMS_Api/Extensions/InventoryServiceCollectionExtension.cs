using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Computerized_maintenance_Logic_layer.Module.DownTimeTracking;
using Computerized_maintenance_Logic_layer.Module.InventoryManagement;
using Computerized_maintenance_Logic_layer.Module.preventiveMaintenanceManagement;

namespace CMMS_Api.Extensions
{
    internal static class InventoryServiceCollectionExtension
    {


        public static IServiceCollection AddInventory(this IServiceCollection Services)
        {

            Services.AddScoped<ClsInventoryItems>();
            Services.AddScoped<ClsInventoryTransactions>();

            return Services;
        }
    }



    internal static class AssetsServiceCollectionExtension
    {


        public static IServiceCollection AddAssets(this IServiceCollection Services)
        {

            Services.AddScoped<clsAssets>();
            Services.AddScoped<ClsAssetImage>();
            Services.AddScoped<clsCategories>();
            Services.AddScoped<clsSubCategories>();
            Services.AddScoped<clsLocations>();

            return Services;
        }
    }


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

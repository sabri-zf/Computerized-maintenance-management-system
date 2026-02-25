using Computerized_maintenance_Logic_layer.Module.InventoryManagement;

namespace CMMS_Api.Extensions
{
    internal  static class InventoryServiceCollectionExtension
    {


        public static IServiceCollection AddInventory(this IServiceCollection Services)
        {

            Services.AddScoped<ClsInventoryItems>();
            Services.AddScoped<ClsInventoryTransactions>();

            return Services;
        }
    }
}

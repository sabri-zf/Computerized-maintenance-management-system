using Computerized_maintenance_Logic_layer.Module.AssetsManagement;

namespace CMMS_Api.Extensions
{
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


  

}

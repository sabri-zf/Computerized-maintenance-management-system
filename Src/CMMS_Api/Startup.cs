
using CMMS_Api.Helper;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Computerized_maintenance_Logic_layer.Module.InventoryManagement;
using Computerized_maintenance_Logic_layer.Module.preventiveMaintenanceManagement;
using Computerized_maintenance_Logic_layer.Module.workOrderManagement;
using computrized_maintenance_Data_Access.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CMMS_Api
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //register Application Classes on DI container to dealing with the opration of Ef core regulary
            builder.Services.AddScoped<ClsWorkorders>();
            builder.Services.AddScoped<ClsWorkOrderParts>();
            builder.Services.AddScoped<ClsWorkOrderHistory>();

            builder.Services.AddScoped<ClsInventoryItems>();
            builder.Services.AddScoped<ClsInventoryTransactions>();

            builder.Services.AddScoped<clsAssets>();
            builder.Services.AddScoped<ClsAssetImage>();
            builder.Services.AddScoped<clsCategories>();
            builder.Services.AddScoped<clsSubCategories>();
            builder.Services.AddScoped<clsLocations>();

            builder.Services.AddScoped<ClspreventiveMaintenances>();

            // Add controller services to the container of DI
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
                {

                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtMapping.Instance!.Issuer,

                        ValidateAudience = true,
                        ValidAudience = JwtMapping.Instance.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtMapping.Instance.IssuerSigningKey))
                    };

                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}


using CMMS_Api.Extensions;
using CMMS_Api.Helper;
using Computerized_maintenance_Logic_layer.Module.DownTimeTracking;
using Computerized_maintenance_Logic_layer.Module.PM_SchedulingManagement;
using Computerized_maintenance_Logic_layer.Module.preventiveMaintenanceManagement;
using Computerized_maintenance_Logic_layer.Module.ReportsAndAnalysis;
using Computerized_maintenance_Logic_layer.Module.User_Management;
using Computerized_maintenance_Logic_layer.Module.User_Management.Extensions;
using Computerized_maintenance_Logic_layer.Services;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement;
using computrized_maintenance_Data_Access.UserManagement;
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
            builder.Services.AddWorkOrders();
            builder.Services.AddInventory();
            builder.Services.AddAssets();
            builder.Services.AddDownTimeAndPriventive();

            builder.Services.AddScoped<CalculateService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ScheduleService>();
            builder.Services.AddScoped<LeapYearService>();
            builder.Services.AddScoped<PreventiveMaintenaceService>();

            builder.Services.AddScoped<AdminRepo>();
            builder.Services.AddScoped<UserRepo>();
            builder.Services.AddScoped<RoleRepo>();

            builder.Services.AddScoped<ClsRoles>();
            builder.Services.AddScoped<ClsReports>();
            builder.Services.AddScoped<ClsAdmins>();
            builder.Services.AddScoped<ClsUsers>();
            builder.Services.AddScoped<ClsPMSchedule>();

            // Add controller services to the container of DI
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // builder.Services.AddAuthentication("Bearer")
            //     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
            //     {

            //         option.TokenValidationParameters = new TokenValidationParameters()
            //         {
            //             ValidateIssuer = true,
            //             ValidIssuer = JwtMapping.Instance!.Issuer,

            //             ValidateAudience = true,
            //             ValidAudience = JwtMapping.Instance.Audience,

            //             ValidateLifetime = true,

            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtMapping.Instance.IssuerSigningKey))
            //         };

            //     });
            // builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Erorr");
                app.UseHsts();
            }
            else
            {
             app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

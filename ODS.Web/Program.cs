using ODS.Web.Brokers.Loggings;
using ODS.Web.Brokers.Storages;
using ODS.Web.Services.Foundations.Employees;
using ODS.Web.Services.Orchestrations.Employees;
using ODS.Web.Services.Processings.Employees;

namespace ODS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StorageBroker>();
            AddBrokers(builder.Services);
            AddFoundationServices(builder.Services);
            AddProcessingServices(builder.Services);
            AddOrchestrationServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
        }

        private static void AddFoundationServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
        }

        private static void AddProcessingServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeProcessingService, EmployeeProcessingService>();
        }

        private static void AddOrchestrationServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeOrchestrationServices, EmployeeOrchestrationServices>();
        }
    }
}
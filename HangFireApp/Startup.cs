using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(HangFireApp.Startup))]

namespace HangFireApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var options = new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = false,
                QueuePollInterval = TimeSpan.FromSeconds(30) // Default value
            };

            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString, options);
            app.UseHangfireServer();
            app.UseHangfireDashboard("/jobs");

            BackgroundJob.Schedule(() => Welcome(), TimeSpan.FromMinutes(1));
        }

        public void Welcome()
        {
            Console.WriteLine("Hello Wold");
            Console.ReadKey();
        }
    }
}

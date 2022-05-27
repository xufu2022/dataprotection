using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataProtectionLib
{
    public static class DataProtectionExtensions
    {
        //public static Dictionary<Projects, dynamic> GetDbContexts()
        //{
        //    var dictionary = new Dictionary<Projects, dynamic>();
        //    dictionary.Add(Projects.TaskManager, typeof(Sample1DbContext));
        //    dictionary.Add(Projects.Crm, typeof(Sample2DbContext));
        //    return dictionary;
        //}

        public static void AddDataProtectionForClient<T>(this IServiceCollection service, IConfiguration configuration,
            string? applicationName) where T : EFDbContext
        {
            if(applicationName ==null)
                return;
            var connectionString = configuration.GetConnectionString("DataProtection");
            service.AddDbContext<T>(options =>
                options.UseSqlServer(connectionString));
            service.AddDataProtection().PersistKeysToDbContext<T>().SetApplicationName(applicationName.ToString());
        }

        public static void ApplyDbMigration<T>(this IApplicationBuilder builder) where T : EFDbContext
        {
            using var scope=builder.ApplicationServices.CreateScope();
            var service = scope.ServiceProvider;
            var context=service.GetRequiredService<T>();
            context.Database.Migrate();
        }
        //public static void AddForClient(this IServiceCollection service, IConfiguration configuration,
        //    string applicationName)
        //{
        //    Enum.TryParse(applicationName, out Projects app);
        //    if (app == Projects.None)
        //        return;
        //    var dbContext = GetDbContexts().First(x => x.Key == app).Value as EFDbContext;
        //    //AddDataProtectionForClient<>(service, configuration, applicationName);
        //}

        public static void AddClientForMvc(this IServiceCollection service, IConfiguration configuration,
            string applicationName)
        {
            Enum.TryParse(applicationName, out Projects app);
            if (app == Projects.None)
                return;
            switch (app)
            {
                case Projects.Crm:
                    AddDataProtectionForClient<CrmWeb_DPDbContext>(service, configuration, Projects.Crm.ToString());
                    break;                
                case Projects.CrmApi:
                    AddDataProtectionForClient<CrmApi_DPDbContext>(service, configuration, Projects.CrmApi.ToString());
                    break;
                case Projects.TaskManager:
                    AddDataProtectionForClient<TaskManager_DPDbContext>(service, configuration, Projects.TaskManager.ToString());
                    break;
                default:
                    break;
            }
           
        }

        public static void ApplyProtectionDb(this IApplicationBuilder builder, string applicationName)
        {
            Enum.TryParse(applicationName, out Projects app);
            if (app == Projects.None)
                return;
            switch (app)
            {
                case Projects.Crm:
                    ApplyDbMigration<CrmWeb_DPDbContext>(builder);
                    break;                
                case Projects.CrmApi:
                    ApplyDbMigration<CrmApi_DPDbContext>(builder);
                    break;
                case Projects.TaskManager:
                    ApplyDbMigration<TaskManager_DPDbContext>(builder);
                    break;
                default:
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                case Projects.TaskManager:
                    AddDataProtectionForClient<TaskManager_DPDbContext>(service, configuration, Projects.TaskManager.ToString());
                    break;
                default:
                    break;
            }
           
        }
    }
}

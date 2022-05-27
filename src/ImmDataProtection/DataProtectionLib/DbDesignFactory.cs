using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataProtectionLib
{
    public class DbDesignFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public TContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            var connString = configuration.GetConnectionString("DataProtection");
                //"server=.;database=placeholder;Integrated Security=true;".Replace("placeholder", "DataProtection");
            var builder = new DbContextOptionsBuilder<TContext>().UseSqlServer(
                connString);
            return (TContext)Activator.CreateInstance(typeof(TContext), new object[] { builder.Options });
        }

    }

    public class TaskManager_DPContextFactory : DbDesignFactory<TaskManager_DPDbContext> { }
    public class CrmWebContextFactory : DbDesignFactory<CrmWeb_DPDbContext> { }
    public class CrmWebApiContextFactory : DbDesignFactory<CrmApi_DPDbContext> { }
    //public  class Sample1{}
    //public class Sample2 { }


    public class TaskManager_DPDbContext : EFDbContext
    {
        public TaskManager_DPDbContext(DbContextOptions<TaskManager_DPDbContext> options) : base(options, Projects.TaskManager)
        {
        }
    }
    public class CrmWeb_DPDbContext : EFDbContext
    {
        public CrmWeb_DPDbContext(DbContextOptions<CrmWeb_DPDbContext> options) : base(options, Projects.Crm)
        {
        }
    }    
    public class CrmApi_DPDbContext : EFDbContext
    {
        public CrmApi_DPDbContext(DbContextOptions<CrmApi_DPDbContext> options) : base(options, Projects.CrmApi)
        {
        }
    }
}

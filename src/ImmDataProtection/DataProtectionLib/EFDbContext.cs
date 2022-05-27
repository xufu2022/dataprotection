using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataProtectionLib
{
    public class EFDbContext: DbContext, IDataProtectionKeyContext
    {
        private Projects _proj;
        public EFDbContext(DbContextOptions options, Projects proj) : base(options)
        {
            this._proj = proj;
        }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataProtectionKey>().ToTable("DataProtectionKey_" + this._proj.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<RealEstateCompany> RealEstateCompanies { get; set; }
        public DbSet<PropertyManager> PropertyManagers { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {

        //     modelBuilder.Entity<User>(entity =>
        //     {
        //         entity.HasKey(u => u.Id);
        //         entity.Property(u => u.UserName).IsRequired();

        //         // Remove the incorrect line or replace it with a valid navigation property if needed
        //     });
        //     base.OnModelCreating(modelBuilder);
        // }

    }
}
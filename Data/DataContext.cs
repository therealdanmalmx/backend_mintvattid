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

    }
}
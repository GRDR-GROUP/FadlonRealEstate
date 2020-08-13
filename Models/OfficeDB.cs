using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FadlonRealEstate.Models
{
    public class OfficeDB : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Deal> Deals { get; set; }

    }
}
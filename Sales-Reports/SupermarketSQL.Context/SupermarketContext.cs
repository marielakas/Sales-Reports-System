using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketSQL.Models;
using System.Data.Entity;

namespace SupermarketSQL.Context
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext()
            : base("Supermarket")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<TaxTable> Taxes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}

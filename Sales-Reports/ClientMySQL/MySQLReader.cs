using Supermarket.Data;
using SupermarketSQL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMySQL
{
    public static class MySQLReader
    {
        public static void MoveData()
        {
            MoveMeasures();
            MoveVendors();
            MoveProducts();
        }
  
        private static void MoveProducts()
        {
            var client = new SupermarketModel();
            using (client)
            {
                var products = client.Products;

                using (SupermarketContext context = new SupermarketContext())
                {
                    foreach (var item in products)
                    {
                        var product = new SupermarketSQL.Models.Product
                        {
                            ProductName = item.ProductName,
                            BasePrice = item.BasePrice,
                            MeasureId = item.MeasureId,
                            VendorId = item.VendorId
                        };
                        context.Products.Add(product);
                    }

                    context.SaveChanges();
                }
            }
        }
  
        private static void MoveVendors()
        {
            var client = new SupermarketModel();
            using (client)
            {
                var vendors = client.Vendors;

                using (SupermarketContext context = new SupermarketContext())
                {
                    foreach (var item in vendors)
                    {
                        var vendor = new SupermarketSQL.Models.Vendor
                        {
                            VendorName = item.VendorName
                        };
                        context.Vendors.Add(vendor);
                    }

                    context.SaveChanges();
                }
            }
        }
  
        private static void MoveMeasures()
        {
            var client = new SupermarketModel();
            using (client)
            {
                var measures = client.Measures;

                using (SupermarketContext context = new SupermarketContext())
                {
                    foreach (var item in measures)
                    {
                        var measure = new SupermarketSQL.Models.Measure { MeasureName = item.MeasureName };
                        context.Measures.Add(measure);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}

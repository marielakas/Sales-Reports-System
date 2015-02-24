using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SupermarketSQL.Models;
using System.IO;
using SupermarketSQL.Context;

namespace ClientMongoDB
{
    public static class MongoDbWriter
    {
        public static void Write()
        {
            var mongoClient = new MongoClient("mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            var supermarketDb = mongoServer.GetDatabase("Supermarket");
            if (!supermarketDb.CollectionExists("ProductReports"))
            {
                supermarketDb.CreateCollection("ProductReports");
            }

            var productReports = supermarketDb.GetCollection("ProductReports");
            productReports.RemoveAll();

            SupermarketContext db = new SupermarketContext();

            var products = db.Sales
                .Include("Product")
                .Select(p => new
                {
                    productId = p.Product.Id,
                    productName = p.Product.ProductName,
                    vendorName = p.Product.Vendor.VendorName,
                    totalQuantitySold =
                        db.Sales
                        .Where(x => x.ProductId == p.Product.Id)
                        .Sum(x => x.Quantity),
                    totalIncomes =
                        db.Sales
                        .Where(x => x.ProductId == p.Product.Id)
                        .Sum(x => x.Quantity * x.UnitPrice)
                })
                .Distinct()
                .ToList();

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[ ");
            foreach (var product in products)
            {
                jsonBuilder.Append(product.ToJson() + ", ");
                var bson = product.ToBsonDocument();
                productReports.Insert(bson);
                //Console.WriteLine(product.productName);
            }

            jsonBuilder.Length -= 2;
            jsonBuilder.Append(" ]");
            //Console.WriteLine(jsonBuilder);

            string dirPath = @"..\..\..\Product-Reports\";
            string fileName = "all-products.json";
            try
            {
                DirectoryInfo dir = Directory.CreateDirectory(dirPath);
                using (StreamWriter output = new StreamWriter(dirPath + fileName))
                {
                    output.Write(jsonBuilder);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

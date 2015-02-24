using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientExcel
{
    public static class ExcelWriter
    {
        public static void CreateExcelTable()
        {
            var mongoClient = new MongoClient("mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            var supermarketDb = mongoServer.GetDatabase("Supermarket");
            if (!supermarketDb.CollectionExists("ProductReports"))
            {
                supermarketDb.CreateCollection("ProductReports");
            }

            string templateFile = @"..\..\..\template.xls";
            string destinationFile = @"..\..\..\Products-Total-Report.xls";

            try
            {
                File.Copy(templateFile, destinationFile, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var productReports = supermarketDb.GetCollection("ProductReports").FindAll().ToList();

            OleDbConnection dbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data Source=..\..\..\Products-Total-Report.xls;Extended Properties='Excel 8.0;HDR=YES'");

            foreach (var report in productReports)
            {
                OleDbCommand myCommand = new OleDbCommand("INSERT INTO [Sheet1$] (Id,ProductId,ProductName,VendorName,TotalQuantitySold,TotalIncomes) " +
                    @"VALUES (@Id,@ProductId,@ProductName,@VendorName,@TotalQuantitySold,@TotalIncomes)", dbConn);
                dbConn.Open();
                foreach (var item in report)
                {
                    //Console.WriteLine(item.Name);
                    //Console.WriteLine(item.Value);
                    if (item.Name == "_id")
                    {
                        myCommand.Parameters.AddWithValue("@Id", item.Value.ToString());
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@" + item.Name, item.Value);
                    }
                }
                //var strCn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sheetToCreate + "; Extended Properties='Excel 8.0;HDR=YES'";
                myCommand.ExecuteNonQuery();
                dbConn.Close();
                //Console.WriteLine();
            }
        }
    }
}

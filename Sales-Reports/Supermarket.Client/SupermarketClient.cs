using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Data;
using System.Data.Entity;
using SupermarketSQL.Context;
using SupermarketSQL.Models;
using ClientMySQL;
using SupermarketSQL.Context.Migrations;
using ClientExcel;
using ClientMongoDB;
using ClientSQLite;
using PDFWriter;

namespace Supermarket.Client
{
    class SupermarketClient
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion
                <SupermarketContext, Configuration>());

            ClientMySQL.MySQLReader.MoveData();
            ExcelReader.ExtractReportNames();
            MongoDbWriter.Write();
            SQLiteReader.MoveData();

            PDFFileCreator.CreateTable("sampleForTest.pdf");
            XMLFIleCreator.CreateXMLFile("salesForTest.xml");

            ExcelWriter.CreateExcelTable();
            XMLFileReader.ParseXML("../../Vendor-Expenses.xml");
        }
    }
}

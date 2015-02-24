using Ionic.Zip;
using SupermarketSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientExcel
{
    public static class ExcelReader
    {
        public static void ExtractReportNames()
        {
            string zipToUnpack = @"..\..\..\Sample-Sales-Reports.zip";
            string unpackDirectory = @"..\..\..\Temp\";

            using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(zipToUnpack))
            {
                foreach (ZipEntry e in zip1)
                {
                    e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                    if (e.FileName.Contains("xls"))
                    {
                        Print_Excel(e.FileName);
                    }
                }
            }
        }

        public static void Print_Excel(string datasource)
        {
            DataTable dt = new DataTable("newtable");
            OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = @"..\..\..\Temp\" + datasource;
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES");
            //Console.WriteLine(datasource);

            using (OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
            {
                connection.Open();
                string selectSql = @"SELECT * FROM [Sales$]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectSql, connection))
                {
                    adapter.FillSchema(dt, SchemaType.Source);
                    adapter.Fill(dt);
                }
                connection.Close();
            }

            string dateStr = datasource.Substring(0, datasource.IndexOf('/'));
            DateTime date = DateTime.Parse(dateStr);
            Sale sale = new Sale();
            sale.Date = date;

            bool endOfTable = false;

            foreach (DataRow row in dt.Rows)
            {
                //Console.WriteLine("-------------");
                //Console.WriteLine(row);
                int countColumn = 1;

                using (var context = new SupermarketSQL.Context.SupermarketContext())
                {
                    foreach (var item in row.ItemArray)
                    {
                        string itemStr = item.ToString();
                        if (itemStr.StartsWith("Total"))
                        {
                            endOfTable = true;
                        }
                        if (itemStr != "")
                        {
                            if (sale.Supermarket == null)
                            {
                                sale.Supermarket = itemStr;
                                //Console.WriteLine(sale.Supermarket);
                            }

                            if (char.IsDigit(itemStr[0]) && !endOfTable)
                            {
                                if (countColumn == 1)
                                {
                                    sale.ProductId = int.Parse(itemStr);
                                    countColumn++;
                                }
                                else if (countColumn == 2)
                                {
                                    sale.Quantity = int.Parse(itemStr);
                                    countColumn++;
                                }
                                else if (countColumn == 3)
                                {
                                    sale.UnitPrice = decimal.Parse(itemStr);
                                    countColumn++;
                                }
                                else if (countColumn == 4)
                                {
                                    sale.Sum = decimal.Parse(itemStr);
                                    countColumn++;

                                    context.Sales.Add(sale);
                                    context.SaveChanges();
                                }
                            }
                            //Console.Write(item + " ");
                        }

                    }
                    //Console.WriteLine();

                }

            }
            //Console.WriteLine("End table \n");
        }
    }
}

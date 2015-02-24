using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SupermarketSQL.Context;
using SupermarketSQL.Models;
using System.IO;

namespace PDFWriter
{
    public static class PDFFileCreator
    {
        public static void CreateTable(string pdfFile)
        {
            SupermarketContext model = new SupermarketContext();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document,
                               new FileStream("../../" + pdfFile, FileMode.Create));
            using (writer)
            {
                document.Open();
                PdfPTable table = new PdfPTable(5);
                table.AddCell("Product");
                table.AddCell("Quantity");
                table.AddCell("Unit Price");
                table.AddCell("Location");
                table.AddCell("Sum");

                var sales = model.Sales.Include("Product").Select(s => s).ToList();

                foreach (var sale in sales)
                {
                    table.AddCell(sale.Product.ProductName.ToString());
                    table.AddCell(sale.Quantity.ToString());
                    table.AddCell(sale.UnitPrice.ToString());
                    table.AddCell(sale.Supermarket.ToString());
                    table.AddCell(sale.Sum.ToString());
                }

                document.Add(table);
                document.Close();
            }
        }
    }
}

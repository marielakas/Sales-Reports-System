using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SupermarketSQL.Models;
using SupermarketSQL.Context;

namespace PDFWriter
{
    public static class XMLFIleCreator
    {
        public static void CreateXMLFile(string path)
        {
            using (SupermarketContext context = new SupermarketContext())
            {
                path = "../../" + path;
                XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                XElement sales = new XElement("sales");
                document.Add(sales);

                var vendors = context.Sales.Include("Vendor").GroupBy(x => x.Product.Vendor.VendorName).ToList();

                foreach (var vendor in vendors)
                {
                    string vendorName = vendor.First().Product.Vendor.VendorName;
                    XElement root = new XElement("sale");
                    root.Add(new XAttribute("vendor", vendorName));

                    foreach (var v in vendor.GroupBy(x => x.Date))
                    {
                        decimal totalSum = 0.0m;

                        foreach (var item in v)
                        {
                            totalSum += item.Sum;
                        }

                        XElement summary = new XElement("summary");
                        string data = v.First().Date.ToString();
                        summary.Add(new XAttribute("total-sum", totalSum.ToString()));
                        summary.Add(new XAttribute("date", data));
                        root.Add(summary);
                        totalSum = 0.0m;
                    }
                    sales.Add(root);
                }
                document.Save(path);
            }
        }
    }
}

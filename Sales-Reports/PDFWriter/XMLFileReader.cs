using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SupermarketSQL.Context;
using SupermarketSQL.Models;
using System.IO;

namespace PDFWriter
{
    public class XMLFileReader
    {
        public static void ParseXML(string filePath)
        {
            StringBuilder result = new StringBuilder();
           

            using (SupermarketContext context = new SupermarketContext())
            {
                string vendor = string.Empty;
                string expence = string.Empty;
                string date = string.Empty;
                
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "sale"))
                        {
                            vendor = (reader.GetAttribute(0)).ToString();
                        }

                        if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "expenses"))
                        {
                            date = (reader.GetAttribute(0)).ToString();
                            expence = (reader.ReadElementString());
                        }

                        Expense expenceObj = new Expense()
                        {
                             Date = date,
                             Expenses = expence,
                             VendorName=vendor
                        };

                        context.Expenses.Add(expenceObj);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}

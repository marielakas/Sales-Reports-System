using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Data;
using SupermarketSQL.Context;

namespace ClientSQLite
{
    public class SQLiteReader
    {
        public static void MoveData()
        {
            var sqliteData = new SupermarketSQLite();
            using (sqliteData)
            {
                var taxes = sqliteData.Taxes;
                using (SupermarketContext context = new SupermarketContext())
                {
                    foreach (var item in taxes)
                    {
                        var tax = new SupermarketSQL.Models.TaxTable
                        {
                            ProductId = item.ProductId,
                            Tax = item.Tax
                        };
                        context.Taxes.Add(tax);
                        sqliteData.SaveChanges();
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}

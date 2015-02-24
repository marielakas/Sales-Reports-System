using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketSQL.Models
{
    public class Expense
    {
        private int _id;
        public virtual int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        private string vendorName;
        public virtual string VendorName
        {
            get
            {
                return this.vendorName;
            }
            set
            {
                this.vendorName = value;
            }
        }

        private string date;
        public virtual string Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }

        private string expenses;
        public virtual string Expenses
        {
            get
            {
                return this.expenses;
            }
            set
            {
                this.expenses = value;
            }
        }
    }
}

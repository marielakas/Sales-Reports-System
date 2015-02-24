using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketSQL.Models
{
    public class Vendor
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

        private string _vendorName;
        public virtual string VendorName
        {
            get
            {
                return this._vendorName;
            }
            set
            {
                this._vendorName = value;
            }
        }

        private IList<Product> _products = new List<Product>();
        public virtual IList<Product> Products
        {
            get
            {
                return this._products;
            }
        }
    }
}

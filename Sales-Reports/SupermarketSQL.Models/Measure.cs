using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketSQL.Models
{
    public class Measure
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

        private string _measureName;
        public virtual string MeasureName
        {
            get
            {
                return this._measureName;
            }
            set
            {
                this._measureName = value;
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

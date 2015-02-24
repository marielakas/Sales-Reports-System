using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketSQL.Models
{
    public class Sale
    {
        private int id;
        public virtual int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private int productId;
        public virtual int ProductId
        {
            get
            {
                return this.productId;
            }
            set
            {
                this.productId = value;
            }
        }

        private int quantity;
        public virtual int Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                this.quantity = value;
            }
        }

        private decimal unitPrice;
        public virtual decimal UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
            set
            {
                this.unitPrice = value;
            }
        }

        private decimal sum;
        public virtual decimal Sum
        {
            get
            {
                return this.sum;
            }
            set
            {
                this.sum = value;
            }
        }

        private string supermarket;
        public virtual string Supermarket
        {
            get
            {
                return this.supermarket;
            }
            set
            {
                this.supermarket = value;
            }
        }

        private DateTime date;
        public virtual DateTime Date
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

        private Product product;
        public virtual Product Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
            }
        }

    }
}

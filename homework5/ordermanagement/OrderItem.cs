using System;
using System.Collections.Generic;
using System.Text;

namespace ordermanagement
{
    class OrderItem
    {
        
        public Product product;
        private int num;

        public OrderItem(string pname, double price, int num)
        {
            this.num = num;
            product = new Product(pname, price);
        }

        public override bool Equals(object obj)
        {
            OrderItem m = obj as OrderItem;
            if (m.Equals(obj) && num == m.num)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public double GetToatalPrice() { 
            return num*product.GetPrice();
        }
        public override string ToString()
        {
            return product.ToString()+"number:"+num;
        }
    }
}

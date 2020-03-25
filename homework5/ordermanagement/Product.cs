using System;
using System.Collections.Generic;
using System.Text;

namespace ordermanagement
{
    class Product
    {
        private string name;
        private double price;
        public Product(string pname, double p)
        {
            name = pname;
            price = p;
        }
        public void SetName(string pname)
        {
            if (pname == null && pname == "")
            {
                Exception e = new Exception("can't change the name into blank");
            }
            name = pname;
        }
        public string GetName()
        {
            return name;
        }
        public double GetPrice()
        {
            return price;
        }
        public void SetPrice(double p)
        {
            if (price <= 0)
            {
                Exception e = new Exception("price can't less than 0");
                throw e;
            }
            price = p;
        }

        public override bool Equals(object obj)
        {
            Product m = obj as Product;
            if (m.name == name && m.price == price)
            {
                return true;
            }
            else return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return " Product: " + name + "  Price: " + price;
        }
    }
}

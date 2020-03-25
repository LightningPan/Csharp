using System;
using System.Collections.Generic;
using System.Text;

namespace ordermanagement
{
    class Customer
    {
        private string name;
        private string address;
        public Customer(string cname,string addr)
        {
            address = addr;
            name = cname;
        }
        public void SetAddress(string addr)
        {
            if (addr == null && addr == "")
            {
                Exception e = new Exception("can't change the address with blank");
            }
            address = addr;
        }
        public string GetAddress()
        {
            return address;
        }
        public void SetName(string cname)
        {
            if (cname == null && cname == "")
            {
                Exception e = new Exception("can't change the name with blank");
            }
            name = cname;
        }
        public string GetName()
        {
            return name;
        }
        public override string ToString()
        {
            return " Customer: " + name+" Address："+address;
        }
    }
}

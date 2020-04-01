using System;
using System.Collections.Generic;
using System.Text;

namespace ordermanagement
{   [Serializable]
    public class Order
    {
        public Customer customer;
        public  int ID { get; set; }
        public List<OrderItem> Item = new List<OrderItem>();

        public Order() { }
        public Order(int n, string cname, string addr)
        {
            ID = n;
            customer = new Customer(cname,addr);
        }

        public double GetTotalBill() {
            double bill=0;
            foreach (OrderItem m in Item) {
                bill += m.GetToatalPrice();
            }
            return bill;
            
        }
        public void AddOrderItem(string pname, double price,int num) {
            Item.Add(new OrderItem(pname, price,num));
        }
        public override bool Equals(object obj)
        {
            Order order = obj as Order;
            return order.ID == ID;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            string s = " ID: " + ID;
            foreach (OrderItem m in Item) {
                s += m.ToString();
            }
            return s;
        }
    }
}

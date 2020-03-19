using System;
using System.Collections.Generic;
using System.Linq;

namespace ordermanagement
{

    public class Product {
        private string name;
        private double price;
        public Product(string pname,double p) {
            name = pname;
            price = p;
        }
        public void SetName(string pname) {
            if (pname==null&&pname=="") {
                Exception e = new Exception("can't change the name into blank");
            }
            name = pname;
        }
        public string GetName() {
            return name;
        }
        public double GetPrice() {
            return price;
        }
        public void SetPrice(double p) {
            if (price <= 0) {
                Exception e = new Exception("price can't less than 0");
                throw e;
            }
            price = p;
        }
        public override string ToString()
        {
            return " Product: "+name+"  Price: "+price;
        }
    }
    public class Customer{
        private string name;
        public Customer(string cname) {
            name = cname;
        }
        public void SetName(string cname) {
            if (cname == null && cname == "")
            {
                Exception e = new Exception("can't change the name into blank");
            }
            name = cname;
        }
        public string GetName() {
            return name;
        }
        public override string ToString()
        {
            return " Customer: " + name;
        }
    }

    public class Order
    {
        public readonly int ID;
        public OrderItem Item;
        public Order(int n, string cname, string pname, double price) {
            ID = n;
            Item = new OrderItem(cname,pname,price);
        
        }
        public override bool Equals(object obj)
        {
            Order order = obj as Order;
            return order.ID == ID && Item.Equals(order.Item);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "ID: " + ID + Item.ToString();
        }
    }
    public class OrderItem
    {
        public Customer customer;
        public Product product;

        public OrderItem(string cname,string pname,double price) {
            customer = new Customer(cname);
            product = new Product(pname, price);
        }
        public override bool Equals(object obj)
        {
            OrderItem m = obj as OrderItem;
            return m.customer.GetName() == customer.GetName() && m.product.GetName() == product.GetName();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return customer.ToString() + product.ToString();
        }
    }
    public class OrderService {
        private List<Order> orders = new List<Order>();
        public void AddOrder(int n, string cname, string pname, double price) {
            Order temp = new Order(n,cname,pname,price);
            foreach (Order m in orders) {
                if (m.Equals(temp)) {
                    return;
                }
            }
            orders.Add(temp);
        }
        public void DeleteOrder(int n) {
            for (int i = 0; i < orders.Count; i++) {
                if (orders[i].ID == n) {
                    orders.RemoveAt(i);
                }
            }
        }
        public string SearchAll()
        {
            string result = "";
            foreach (Order s in orders) {
                result += s.ToString();
            }
            return result;
        }
        public string SearchByID(int ID) {
            Order result = (from s in orders where s.ID == ID select s) as Order;
            return result.ToString();
        }
        public string SearchByProduct(string name)
        {
            var result = from s in orders where s.Item.product.GetName() == name orderby s.Item.product.GetPrice() select s;
            List<Order> a = result.ToList();
            string r="";
            foreach (Order m in a) {
                r += m.ToString();
            }
            return r;
        }
        public string SearchByCustomer(string name)
        {
            var result = from s in orders where s.Item.customer.GetName() == name orderby s.Item.product.GetPrice() select s;
            List<Order> a = result.ToList();
            string r = "";
            foreach (Order m in a)
            {
                r += m.ToString();
            }
            return r;
        }

        public void Sort() {
            orders.Sort((p1,p2)=>p1.ID-p2.ID);
        }
        
        public void Sort(Comparison<Order> T) {
            orders.Sort(T);
        }
        public void SetCustomerName(int n,string name) {
            int i = 0;
            for (; i < orders.Count; i++)
            {
                if (orders[i].ID == n){
                    orders[i].Item.customer.SetName(name);
                    return;
                }
            }
            if (i == orders.Count) {
                Exception e = new Exception("No such order! Please check your ID.");
                throw e;
            }
        }
        public void SetProductName(int n, string name)
        {
            int i = 0;
            for (; i < orders.Count; i++)
            {
                if (orders[i].ID == n)
                {
                    orders[i].Item.product.SetName(name);
                    return;
                }
            }
            if (i == orders.Count)
            {
                Exception e = new Exception("No such order! Please check your ID.");
                throw e;
            }
        }
        public void SetProductPrice(int n, double price)
        {
            int i = 0;
            for (; i < orders.Count; i++)
            {
                if (orders[i].ID == n)
                {
                    orders[i].Item.product.SetPrice(price);
                    return;
                }
            }
            if (i == orders.Count)
            {
                Exception e = new Exception("No such order! Please check your ID.");
                throw e;
            }
        }


    }











    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}

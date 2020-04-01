using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace ordermanagement
{
   public class OrderService
    {
        private List<Order> orders = new List<Order>();
        public void AddOrder(Order order)
        {
            foreach (Order m in orders) {
                if (m.Equals(order)) return;
            }
            orders.Add(order);
        }

        public void DeleteOrder(int n)  //根据ID删除订单
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].ID == n)
                {
                    orders.RemoveAt(i);
                }
            }
        }
        public string SearchAll()
        {
            string result = "";
            foreach (Order s in orders)
            {
                result += s.ToString();
            }
            return result;
        }
        public string SearchByID(int ID)
        {
            Order result = (from s in orders where s.ID == ID select s) as Order;
            return result.ToString();
        }
        public string SearchByProduct(string name)
        {
            string result1 = "";
            foreach (Order m in orders) {
                var result = from s in m.Item where s.product.GetName()== name orderby s.product.GetPrice() select s;
                List<OrderItem> a = result.ToList();
                foreach (OrderItem q in a) {
                    if (m.Item.Contains(q)) {
                        result1 += m.ID+" ";
                    }
                } 
            }
            return result1; ;
        }
        public string SearchByCustomer(string name)
        {
            var result = from s in orders where s.customer.name == name orderby s.GetTotalBill() select s;
            List<Order> a = result.ToList();
            string r = "";
            foreach (Order m in a)
            {
                r += m.ID+" ";
            }
            return r;
        }

        public void Sort()
        {
            orders.Sort((p1, p2) => p1.ID - p2.ID);
        }

        public void Sort(Comparison<Order> T)
        {
            orders.Sort(T);
        }
        public void SetCustomerName(int n, string name)
        {
            int i = 0;
            for (; i < orders.Count; i++)
            {
                if (orders[i].ID == n)
                {
                    orders[i].customer.name=name;
                    return;
                }
            }
            if (i == orders.Count)
            {
                Exception e = new Exception("No such order! Please check your ID.");
                throw e;
            }
        }
       
        public void SetProductPrice(int n, string name,double price)
        {
            int i = 0;
            for (; i < orders.Count; i++)
            {
                if (orders[i].ID == n)
                {
                    int j = 0;
                    for (; j<orders[i].Item.Count;j++) {

                        if (orders[i].Item[j].product.GetName() == name) {

                            orders[i].Item[j].product.SetPrice(price);
                        }
                    }
                    if (j == orders[i].Item.Count) {
                        Exception e = new Exception("No such product! Please check your product.");
                        throw e;
                    }
                }
            }
            if (i == orders.Count)
            {
                Exception e = new Exception("No such order! Please check your ID.");
                throw e;
            }
        }
        public void Export(string s) {
            XmlSerializer xmlSeriallizer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(s, FileMode.Create)) {
                xmlSeriallizer.Serialize(fs,orders);
            }
        }

        public void Import(string s) {
            XmlSerializer xmlSeriallizer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(s, FileMode.Open))
            {
                List<Order> a = (List<Order>)xmlSeriallizer.Deserialize(fs);
                a.ForEach(order => { orders.Add(order); });
            }
        }
    }
}

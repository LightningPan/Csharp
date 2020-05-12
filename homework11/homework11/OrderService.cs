using Org.BouncyCastle.Bcpg;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework11
{
    class OrderService
    {
        int newOrderId = 0;
        public void AddOrder(String CustomerName) {
            using (var context = new OrderContext())
            {
                var order = new Order { OrderId = newOrderId, Customer = CustomerName };
                context.Orders.Add(order);
                context.SaveChanges();
                newOrderId++;
            }
        }

        public void DeleteOrderById(int id) {
            using (var context = new OrderContext()) {
                var order = context.Orders.Include("OrderItems").FirstOrDefault(p => p.OrderId == id);
                if (order != null) {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }
        
        }
        public void DeleteOrderByCustomerName(string name)
        {
            using (var context = new OrderContext())
            {
                var order = context.Orders.Include("OrderItems").FirstOrDefault(p => p.Customer == name);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }

        }

        public void AddOrderItem(int index,string goodsname, int quantity, double price, int orderid) {
            using (var context = new OrderContext()) {
                var orderitem = new OrderItem() { Index=index,GoodsName=goodsname,Quantity=quantity,Price=price,OrderId=orderid};
                context.Entry(orderitem).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        public void UpdateOrder(Order newOrder)
        {
            Order oldOrder = SearchByOrderId(newOrder.OrderId);
            if (oldOrder == null)
                throw new ApplicationException($"Update Error：the order with id {newOrder.OrderId} does not exist!");
            DeleteOrderById(newOrder.OrderId);
            using (var context = new OrderContext())
            {
                context.Orders.Add(newOrder);
                context.SaveChanges();
            }
        }

        public Order SearchByOrderId(int id) {
            using (var context = new OrderContext()) {
                var order = context.Orders.SingleOrDefault(b => b.OrderId == id);
                 return order;
            }
        }

        public Order SearchByCustomerName(string name) {
            using (var context = new OrderContext())
            {
                var order = context.Orders.SingleOrDefault(b => b.Customer == name);
                return order;
            }
        }

        public List<Order> SearchByProductName(string name) {
            using (var context = new OrderContext())
            {
                var query = context.OrderItems.Where(p => p.GoodsName == name).OrderBy(p=>p.Index);
                List<Order> a = new List<Order>();
                foreach (OrderItem Temp in query) {
                    a.Add(Temp.Order);
                }
                return a;
            }
        }

        public List<Order> GetAll() {
            using (var context = new OrderContext()) {
                List<Order> b = new List<Order>();
                foreach (Order a in context.Orders) {
                    b.Add(a);
                }
                return b;
            }

        }
    }
}

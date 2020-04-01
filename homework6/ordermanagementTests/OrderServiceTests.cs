using Microsoft.VisualStudio.TestTools.UnitTesting;
using ordermanagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ordermanagement.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        OrderService test = new OrderService();
        Order a = new Order(1,"a","wall street");
        Order b = new Order(2, "b", "wall street2");
        Order c = new Order(3, "c", "wall street3");
        OrderItem a1 = new OrderItem("apple",21.2,20);
        OrderItem b1 = new OrderItem("apple2", 21.3, 21);
        OrderItem c1 = new OrderItem("apple3", 21.4, 24);
        

        [TestMethod()]
        public void AddOrderTest()
        {
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            test.AddOrder(a);
            test.AddOrder(b);
            test.DeleteOrder(2);
        }

        [TestMethod()]
        public void SearchAllTest()
        {
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
            test.SearchAll();
        }

        [TestMethod()]
        public void SearchByIDTest()
        {
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
            test.SearchByID(2);
        }

        [TestMethod()]
        public void SearchByProductTest()
        {
            a.Item.Add(a1);
            a.Item.Add(b1);
            a.Item.Add(b1);
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
            test.SearchByProduct("apple");
        }

        [TestMethod()]
        public void SearchByCustomerTest()
        {
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
            test.SearchByCustomer("a");
        }

        [TestMethod()]
        public void SortTest()
        {
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
            test.Sort();
        }

        [TestMethod()]
        public void SortTest1()
        {
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
            test.Sort((p1, p2) => p1.ID - p2.ID);
        }

        [TestMethod()]
        public void SetCustomerNameTest()
        {
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
            test.SetCustomerName(1,"qq");
        }

        [TestMethod()]
        public void SetProductPriceTest()
        {
            a.Item.Add(a1);
            a.Item.Add(b1);
            a.Item.Add(b1);
            test.AddOrder(a);
            test.AddOrder(b);
            test.AddOrder(c);
            test.SetProductPrice(1,"apple",30);
        }

        [TestMethod()]
        public void ExportTest()
        {
            String file = "temp.xml";
            test.Export(file);
            List<String> expectLines = File.ReadLines("expectedOrders.xml").ToList();
            List<String> outputLines = File.ReadLines(file).ToList();
            Assert.AreEqual(expectLines.Count, outputLines.Count);
            for (int i = 0; i < expectLines.Count; i++)
            {
                Assert.AreEqual(expectLines[i].Trim(), outputLines[i].Trim());
            }
        }

        [TestMethod()]
        public void ImportTest()
        {
            OrderService os = new OrderService();
            os.Import("./temp.xml");
        }
    }
}
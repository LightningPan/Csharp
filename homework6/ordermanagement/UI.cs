using System;
using System.Collections.Generic;
using System.Text;

namespace ordermanagement
{
    class UI
    {
        OrderService service;
        public UI() {
            service = new OrderService();
        
        }
        public void Start(){

            while (true)
            {
                Console.WriteLine("新增1" + " 查询2" + " 删除3" + " 修改4" + "退出5");
                string s = Console.ReadLine();
                switch (s)
                {
                    case "1": { Add(); break; }
                    case "2":
                        {
                            Console.WriteLine("查询所有1" + " 按ID查询2" + " 按产品名查询3" + " 按客户查询4");
                            Search();
                            break;
                        }
                    case "3": {
                            DeleteOrder();
                            break;
                        }
                    case "4": {
                            Console.WriteLine("修改单价1"+" 修改用户名2");
                            Set();
                            break;
                        }
                    case "5":return;
                    default:
                        {
                            Console.WriteLine("输入有误，请重新输入");
                            break;
                        }
                }
            }
        }

        public void Set() {
            string s = Console.ReadLine();
            switch(s){
                case "1": {
                        Console.WriteLine("请输入ID");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("请输入产品名称");
                        string a=Console.ReadLine();
                        Console.WriteLine("请输入要修改的产品价格");
                        double b =double.Parse( Console.ReadLine());
                        service.SetProductPrice(id,a,b) ;
                        break;
                    }
                case "2": {
                        Console.WriteLine("请输入ID");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("请输入要修改的名称");
                        string a = Console.ReadLine();
                        service.SetCustomerName(id,a);
                        break;
                        }
                default:
                    Console.WriteLine("输入有误请重新输入");
                    break;
            }
        
        }


        public void DeleteOrder() {

            Console.WriteLine("请输入要删除的订单的订单号");
            int s = int.Parse(Console.ReadLine());
            service.DeleteOrder(s);
        
        }
        public void Search() {
            string a=Console.ReadLine();
            switch (a) {
                case "1":Console.WriteLine(service.SearchAll());break;
                case "2": {
                        Console.WriteLine("请输入ID");
                        int s=int.Parse(Console.ReadLine());
                        Console.WriteLine(service.SearchByID(s));
                        break;}
                case "4": {
                        Console.WriteLine("请输入用户名");
                        string s = Console.ReadLine();
                        Console.WriteLine(service.SearchByCustomer(s));
                        break;
                    }
                case "3": {
                        Console.WriteLine("请输入产品名");
                        string s = Console.ReadLine();
                        Console.WriteLine(service.SearchByProduct(s));
                        break;
                    }
            }       
        }

        public void Add() {
            Random s = new Random();
            int ID = s.Next();
            Console.WriteLine("请输入客户姓名：");
            string cname = Console.ReadLine();
            Console.WriteLine("请输入客户地址：");
            string address = Console.ReadLine();
            Console.WriteLine("请您按产品名，订购数量，单价分别输入，输入n继续下一个产品信息录入，输入其他将退出产品信息录入");
            Order order = new Order(ID, cname, address);
            while (true) {
                try
                {
                    string pname = Console.ReadLine();
                    int num = int.Parse(Console.ReadLine());
                    double price = double.Parse(Console.ReadLine());
                    order.AddOrderItem(pname,price,num);
                }
                catch (Exception e) { Console.WriteLine("输入有误，请重新输入"); }
                if (Console.ReadLine() != "n") { break; } 
            }
            service.AddOrder(order);
        }
        
    }
}

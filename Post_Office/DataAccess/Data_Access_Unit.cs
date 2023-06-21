using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DataAccess.Models;

namespace DataAccess
{
    public abstract class Data_Access_Unit
    {
        public static ObservableCollection<Employee> Employees_Buffer=new ObservableCollection<Employee>();
        public static ObservableCollection<Customer> Customer_Buffer=new ObservableCollection<Customer>();
        public static ObservableCollection<Order> Order_Buffer=new ObservableCollection<Order>();
        public static void Read_From_DB()
        {
            Customer me = new Customer("Ali", "Momen", "aalimomen110@gmail.com", "0025445782", "09120133780");
            Customer_Buffer.Add(me);
        }
        ////////////////
        public static void Add_New_Customer(string firstname,string lastname,string ssn,string email,string phone_number)
        {
            Customer New_Customer = new Customer(firstname, lastname, email, ssn, phone_number);
            Customer_Buffer.Add(New_Customer);
        }
        public static void Add_New_Order(string SSN, string sender, string receiver, Box_Content x, bool isexpensive, double weight, Post_Type type,string Phone_Num="")
        {
            var customer = Search_Customer_ID(SSN);
            int price = (int)Order.Calculate_Price(x, isexpensive, weight, type);
            if (customer.Get_My_Blance() < price)
                throw new Exception("Customer doesn't have enough Balance");
            Order New_Order = new Order(SSN, sender, receiver, x, isexpensive, weight, type,Phone_Num);
            New_Order.This_Order_Price = price;
            customer.Add_New_Order_To_My_Orders(New_Order);
            customer.Withdraw_Money(price);
            Order_Buffer.Add(New_Order);
            
        }
        public static Customer Search_Customer_ID(string SSN)
        {
            var Customer = Customer_Buffer.First(x => x._SSN == SSN);
            if (Customer == null)
                throw new Exception();
            else
                return Customer;
        }

        //A function That outputs a list of Orders with required parameters
        public static List<Order> Filtered_Orders(bool Use_SSN, bool Use_Content, bool Use_Price, bool Use_Weight, bool Use_Post_Type, string SSN="",Box_Content Content=Box_Content.Object,int Price=0,double Weight=0,Post_Type Post_type=Post_Type.Normal )
        {
            if (Order_Buffer.Count == 0)
                throw new Exception("No order is registered yet");
            var Filtered_orders = new List<Order>();
            var Orders = Order_Buffer.ToList();
            if (Use_SSN)
                Orders = Orders.Where(x => x.SSN == SSN).ToList();
            if(Use_Content)
                Orders = Orders.Where(x => x.MyBox==Content).ToList();
            if(Use_Price)
                Orders = Orders.Where(x => x.This_Order_Price>=Price).ToList();
            if(Use_Weight)
                Orders = Orders.Where(x => x.Box_Weight >= Weight).ToList();
            if(Use_Post_Type)
                Orders = Orders.Where(x => x.Type == Post_type).ToList();

            Filtered_orders = Orders;




            return Filtered_orders;
        }
        public static Order Show_Status(int ID)
        {
            var this_Order = Order_Buffer.First(x => x._ID == ID);
            if (this_Order == null)
                throw new Exception("Nothing found");
            else
                return this_Order;
        }
        public static List<Order> Display_My_Orders(string SSN)
        {
            var Filtered_orders = new List<Order>();
            foreach (var i in Order_Buffer)
            {
                if (i.SSN == SSN) Filtered_orders.Add(i);
            }

            return Filtered_orders;
        }
    }
}

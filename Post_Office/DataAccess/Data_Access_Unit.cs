using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            customer.Add_New_Order_To_My_Orders(New_Order);
            
        }
        public static Customer Search_Customer_ID(string SSN)
        {
            var Customer = Customer_Buffer.First(x => x._SSN == SSN);
            if (Customer == null)
                throw new Exception();
            else
                return Customer;
        }
    }
}

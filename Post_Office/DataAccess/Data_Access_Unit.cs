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
    }
}

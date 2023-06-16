using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Customer
    {
        private ObservableCollection<Order> My_Orders = new ObservableCollection<Order>();
        private static int Customers_Num = 0;
        private string First_Name = "", Last_Name = "", Email = "";
        private string SSN="",Phone_Number="";
        private string User_Name="", Password="";
        private Wallet MyWallet;
        public string _First_Name
        {
            get { return First_Name; }
            set
            {
                if (Validity.Name_Isvalid(value))
                    First_Name = value;
                else
                    throw new Exception("Invalid pattern for FirstName");

            }
        }
        public string _Last_Name
        {
            get { return Last_Name; }
            set
            {
                if (Validity.Name_Isvalid(value))
                    Last_Name = value;
                else
                    throw new Exception("Invalid pattern for LastName");

            }
        }
        public string _Email
        {
            get { return Email; }
            set
            {
                if (Validity.Email_Isvalid(value))
                    Email = value;
                else
                    throw new Exception("Invalid pattern for Email");
            }
        }
        public string _SSN
        {
            get { return SSN; }
            set
            {
                if (Validity.SSN_Isvalid(value))
                    SSN = value;
                else
                    throw new Exception("Invalid pattern for SSN");
            }
        }
        public string _Phone_Number
        {
            get { return Phone_Number; }
            set
            {
                if (Validity.Phone_Number_Isvalid(value))
                    Phone_Number = value;
                else
                    throw new Exception("Invalid pattern for Phone Number");
            }
        }
/*        public string _Password
        {
            get { return Password; }
            set { Password = value; }
        }*/
        public string _User_Name
        {
            get { return User_Name; }
            set { User_Name = value; }
        }

        public Customer(string first_Name, string last_Name, string email, string ssn,string phonenum,string Pass=null,string User=null)
        {
            _First_Name = first_Name;
            _Last_Name = last_Name;
            _Email = email;
            _SSN = ssn;
            _Phone_Number = phonenum;
            if ((Pass != null) && (User != null))
            {
                this.Password = Pass;
                this.User_Name = User;
            }
            else
            {
                Generate_Set_Password_UserName();
                Customers_Num++;
            }
            MyWallet = new Wallet();

        }
        public void Generate_Set_Password_UserName()
        {
            Random Random_Pass = new Random();
            //generate lastname

            string Generated_Password = "";
            Generated_Password = ((Random_Pass.Next(0, 99999999)).ToString()).PadLeft(8,'0');

            //Generate username
            string Generated_UserName = "user";
            string number = (Customer.Customers_Num.ToString()).PadLeft(4, '0');
            Generated_UserName += number;

            this.Password = Generated_Password;
            this.User_Name = Generated_UserName;
        }
        public void Add_New_Order_To_My_Orders(Order x)
        {
            My_Orders.Add(x);
        }
        public double Get_My_Blance()
        {
            return MyWallet.Get_Balance();
        }
    }
}

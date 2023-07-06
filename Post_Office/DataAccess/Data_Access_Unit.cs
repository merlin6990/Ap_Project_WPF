using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Models;
using System.Net.Mail;
using System.Net;
using iTextSharp.text.pdf;


namespace DataAccess
{
    public abstract class Data_Access_Unit
    {
        public static ObservableCollection<Employee> Employees_Buffer=new ObservableCollection<Employee>();
        public static ObservableCollection<Customer> Customer_Buffer=new ObservableCollection<Customer>();
        public static ObservableCollection<Order> Order_Buffer=new ObservableCollection<Order>();
        public static void Read_From_DB()
        {
            Send_Email();
            //we open our Connection
            string Connect_Str = "Data Source=DESKTOP-9AMR7EU;Initial Catalog=Post_Office_db;Integrated Security=true;";
            SqlConnection Con = new SqlConnection(Connect_Str);
            Con.Open();


            string Command_Orders = "Select * From T_My_Orders";
            string Command_Employee = "Select * From T_Employees";
            string Command_Customer = "Select * From T_Customers";
            //Load customers
            SqlCommand Load_Command_Customer = new SqlCommand(Command_Customer, Con);
            SqlDataReader Read_Customer=Load_Command_Customer.ExecuteReader();
            while (Read_Customer.Read())
            {        
                Customer Pivot = new Customer(Read_Customer.GetValue(0).ToString(), Read_Customer.GetValue(1).ToString(), Read_Customer.GetValue(2).ToString(), Read_Customer.GetValue(3).ToString(), Read_Customer.GetValue(4).ToString(), Read_Customer.GetValue(5).ToString(), Read_Customer.GetValue(6).ToString());
                if (!(Read_Customer.GetValue(7) is DBNull))
                    Pivot.Set(int.Parse(Read_Customer.GetValue(7).ToString()));
                Customer_Buffer.Add(Pivot);
            }
            Read_Customer.Close();
            //Load Employees
            SqlCommand Load_Command_Employee=new SqlCommand(Command_Employee,Con);
            SqlDataReader Read_Employee=Load_Command_Employee.ExecuteReader();
            while (Read_Employee.Read())
            {
                Employee Pivot = new Employee(Read_Employee.GetValue(0).ToString(), Read_Employee.GetValue(1).ToString(), Read_Employee.GetValue(2).ToString(), Read_Employee.GetValue(3).ToString(), Read_Employee.GetValue(4).ToString(), Read_Employee.GetValue(5).ToString());
                Employees_Buffer.Add(Pivot);
            }
            Read_Employee.Close();
            //Load Orders
           SqlCommand Load_Command_Order = new SqlCommand(Command_Orders, Con);
            SqlDataReader Read_Order = Load_Command_Order.ExecuteReader();
            
            while (Read_Order.Read())
            {
                string SSN = Read_Order.GetValue(0).ToString();
                string sender= Read_Order.GetValue(1).ToString();
                string receiver= Read_Order.GetValue(2).ToString();
                Box_Content x = Parser.Cont_Parser(Read_Order.GetValue(3).ToString());
                bool isexpensive=true;
                if (Read_Order.GetValue(4).ToString() == "false")
                    isexpensive = false;
                double weight = double.Parse(Read_Order.GetValue(5).ToString());
                Post_Type type = Parser.Type_Parser(Read_Order.GetValue(6).ToString());
                Status This = Parser.Status_Parser(Read_Order.GetValue(7).ToString());
                string ID_Read = Read_Order.GetValue(8).ToString();
                string Phone_Num = Read_Order.GetValue(9).ToString();
                Order Pivot = new Order(SSN,sender,receiver,x,isexpensive,weight,type,Phone_Num,ID_Read,This);
                if (!(Read_Order.GetValue(10) is DBNull))
                    Pivot.Customer_Comment = Read_Order.GetValue(10).ToString();
                Order_Buffer.Add(Pivot);
                foreach (var customer in Customer_Buffer)
                {
                    if (customer._SSN == SSN)
                    {
                        customer.Add_New_Order_To_My_Orders(Pivot);
                    }
                }
            }
            Read_Order.Close();
            Con.Close();
        }
        ////////////////
        public static void Add_New_Customer(string firstname,string lastname,string ssn,string email,string phone_number)
        {
            Customer New_Customer = new Customer(firstname, lastname, email, ssn, phone_number);
            Customer_Buffer.Add(New_Customer);
            //Change DB
            string Connect_Str = "Data Source=DESKTOP-9AMR7EU;Initial Catalog=Post_Office_db;Integrated Security=true;";
            SqlConnection Con = new SqlConnection(Connect_Str);
            Con.Open();
            string Sql_Command = "Insert Into T_Customers Values ('";
            Sql_Command += firstname + "','" + lastname + "','" + email + "','" + ssn + "','" + phone_number + "','" + New_Customer.Get_Password() + "','" + New_Customer._User_Name+ "','"+ New_Customer.Get_My_Blance().ToString() + "')";
            SqlCommand My_Command = new SqlCommand(Sql_Command, Con);
            My_Command.ExecuteNonQuery();

        }
        public static void Add_New_Emp(Employee NEW)
        {
            Employees_Buffer.Add(NEW);
            //Change DB
            string Connect_Str = "Data Source=DESKTOP-9AMR7EU;Initial Catalog=Post_Office_db;Integrated Security=true;";
            SqlConnection Con = new SqlConnection(Connect_Str);
            Con.Open();
            string Sql_Command = "Insert Into T_Employees Values ('";
            Sql_Command += NEW._First_Name + "','" + NEW._Last_Name + "','" + NEW._Email + "','" + NEW._ID + "','" + NEW._UserName + "','" + NEW._Password + "')";
            SqlCommand My_Command = new SqlCommand(Sql_Command, Con);
            My_Command.ExecuteNonQuery();
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
            string Boolean="false";
            if (isexpensive == true)
                Boolean = "true";
            //change DB
            string Connect_Str = "Data Source=DESKTOP-9AMR7EU;Initial Catalog=Post_Office_db;Integrated Security=true;";
            SqlConnection Con = new SqlConnection(Connect_Str);
            Con.Open();
            string Sql_Command = "Insert Into T_My_Orders Values ('";
            Sql_Command += SSN + "','" + sender + "','" + receiver + "','" + x.ToString() + "','" + Boolean + "','" + weight.ToString() + "','" + type.ToString() + "','" + Status.Registered.ToString() + "','" + New_Order._ID + "','" + Phone_Num +""+"','" + "')";
            SqlCommand My_Command = new SqlCommand(Sql_Command, Con);
            My_Command.ExecuteNonQuery();
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
        public static List<Order> Filtered_Orders(bool Use_SSN, bool Use_Content, bool Use_Price, bool Use_Weight, bool Use_Post_Type, string SSN="",Box_Content Content=Box_Content.Object,int Price=0,double Weight=0,Post_Type Post_type=Post_Type.Normal,bool Report=false )
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

            
            if (Report)
            {

                StreamWriter Writer = new StreamWriter(@"E:\Ap_Project_WPF\Post_Office\DataAccess\Reports\Report.csv");
                Writer.WriteLine("ID;Sender;Receiver;Content;Post_Type;SSN;Status;Weight;Price");
                foreach(var ord in Orders)
                {
                    Writer.WriteLine(ord._ID + ";" + ord.Sender + ";" + ord.Receiver + ";" + ord.Content + ";" + ord.Post_type + ";" + ord.SSN + ";" + ord._Status + ";" + ord.Weight + ";" + ord.Price);
                }
                Writer.Close();
            }
            

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
                if (i.SSN == SSN) 
                    Filtered_orders.Add(i);
            }

            return Filtered_orders;
        }
        public static Customer Log_IN_SearchC(string user,string pass)
        {
            foreach(var i in Customer_Buffer)
            {
                if ((user == i._User_Name) && (pass == i.Get_Password()))
                    return i;
            }
            return null;
        }
        public static Employee Log_IN_SearchU(string user, string pass)
        {
            foreach (var i in Employees_Buffer)
            {
                if ((user == i._UserName) && (pass == i._Password))
                    return i;
            }
            return null;
        }
        public static void Set_Comment(Order x,string comment)
        {
            if (x._Status != Status.Received)
                throw new Exception("You can not comment on a order you havn't received yet");
            x.Customer_Comment = comment;
            string Connect_Str = "Data Source=DESKTOP-9AMR7EU;Initial Catalog=Post_Office_db;Integrated Security=true;";
            SqlConnection Con = new SqlConnection(Connect_Str);
            Con.Open();
            string Sql_Command = "Update T_My_Orders Set Comment ='";
            Sql_Command += comment+"'";
            Sql_Command += " Where ID = " + x._ID.ToString();
            SqlCommand My_Command = new SqlCommand(Sql_Command, Con);
            My_Command.ExecuteNonQuery();

        }
        public static void Charge_Account(Customer X,string Card_Num,string year,string month,string CVV2,string Money,bool receipt=false)
        {
            if (!Validity.Card_Num_Isvalid(Card_Num))
                throw new Exception("Card Number is not Valid");
            if(!Validity.Date_Isvalid(year,month))
                throw new Exception("Date is not Valid");
            if (!Validity.CVV2_Isvalid(CVV2))
                throw new Exception("CVV2 is not Valid");
            if (int.Parse(Money)>=10000 || int.Parse(Money)<=9)
                throw new Exception("Amount of money must be a number between 10 and 99000");
            X.Charge_Wallet(int.Parse(Money));
            if (receipt)
            {
                string Text = "*******************************Receipt Report********************************"+"\n";
                Text += "Customer Name:****************************************************" + X._First_Name + "\n";
                Text += "Date & Time:****************************************************" + DateTime.Now.ToString() + "\n";
                Text += "Money Amount:****************************************************" + Money + "$" + "\n";
                Text += "Wallet Balance***************************************************" + X.Get_My_Blance().ToString() + "$" + "\n";
                Text += "*******************************Compiled By BlackPhoenix********************************";
                iTextSharp.text.Document oDoc = new iTextSharp.text.Document();
                PdfWriter.GetInstance(oDoc, new FileStream("Receipt.pdf", FileMode.Create));
                oDoc.Open();
                oDoc.Add(new iTextSharp.text.Paragraph(Text));
                oDoc.Close();
            }

            string Connect_Str = "Data Source=DESKTOP-9AMR7EU;Initial Catalog=Post_Office_db;Integrated Security=true;";
            SqlConnection Con = new SqlConnection(Connect_Str);
            Con.Open();
            string Sql_Command = "Update T_Customers Set Balance ='";
            Sql_Command += X.Get_My_Blance().ToString() +"'";
            Sql_Command += " Where SSN = " + "'" + X._SSN + "'";
            SqlCommand My_Command = new SqlCommand(Sql_Command, Con);
            My_Command.ExecuteNonQuery();
        }
        public static void Change_Info_2(Customer X,string User, string Pass)
        {
            foreach(var person in Customer_Buffer)
            {
                if (person._User_Name == User)
                    throw new Exception("An account with this username already Exists");
            }
            foreach (var person in Employees_Buffer)
            {
                if (person._UserName == User)
                    throw new Exception("An account with this username already Exists");
            }
            X.Change_Info(User,Pass);
            string Connect_Str = "Data Source=DESKTOP-9AMR7EU;Initial Catalog=Post_Office_db;Integrated Security=true;";
            SqlConnection Con = new SqlConnection(Connect_Str);
            Con.Open();
            string Sql_Command = "Update T_Customers Set User_Name ='";
            Sql_Command += User + "' ,"+"Password ='"+Pass+"'";
            Sql_Command += " Where SSN = " +"'"+X._SSN+"'";
            SqlCommand My_Command = new SqlCommand(Sql_Command, Con);
            My_Command.ExecuteNonQuery();
        }
        public static void Change_Status(Order x,Status This)
        {
            string Connect_Str = "Data Source=DESKTOP-9AMR7EU;Initial Catalog=Post_Office_db;Integrated Security=true;";
            SqlConnection Con = new SqlConnection(Connect_Str);
            Con.Open();
            string Sql_Command = "Update T_My_Orders Set Status ='";
            Sql_Command += This.ToString() + "'";
            Sql_Command += " Where ID = " + x._ID.ToString();
            SqlCommand My_Command = new SqlCommand(Sql_Command, Con);
            My_Command.ExecuteNonQuery();
        }
        public static void Send_Email(/*Customer X,string Subject,string Body*/)
        {
/*            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("aalimomen110@gmail.com");
            mailMessage.To.Add("bihovam174@devswp.com");
            mailMessage.Subject = "Subject";
            mailMessage.Body = "This is test email";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("username", "password");
            smtpClient.EnableSsl = true;

            smtpClient.Send(mailMessage);*/

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public enum Status
    {
        Registered=0,
        Ready_To_Ship,
        Shipped,
        Received
    }
    public enum Box_Content
    {
        Object=0,
        Document,
        Fragile
    }
    public enum Post_Type
    {
        Normal=0,
        Premium
    }
    public class Order
    {
        private Status This_Order_Status = Status.Registered;
        public string Customer_Comment="";
        public Status _Status
        {
            get { return This_Order_Status; }
            set
            {
                    This_Order_Status = value;
            }
        }
        public int This_Order_Price;
        private string Additional_Phone_Num;
        private string Customer_SSN;
        private string Sender_Address, Receiver_Address;
        public Box_Content MyBox;
        bool Content_Isexpensive;
        public double Box_Weight;
        public Post_Type Type;
        private int ID;
        private static int ID_num = 0;
        public int _ID
        {
            get { return ID; }
        }
        public string SSN
        {
            get { return Customer_SSN; }
        }
        public string Sender
        {
            get { return Sender_Address; }
        }
        public string Receiver
        {
            get { return Receiver_Address; }
            set { Receiver_Address = value; }
        }
        public Post_Type Post_type
        {
            get { return Type; }
        }
        public Box_Content Content
        {
            get { return MyBox; }
        }
        public int Price
        {
            get { return This_Order_Price; }
        }
        public double Weight
        {
            get { return Box_Weight; }
        }
        public Order(string SSN,string sender,string receiver,Box_Content x,bool isexpensive,double weight,Post_Type type, string Phone_Num = "")
        {
            if (weight <= 0)
                throw new ArgumentException("Weight of the box cannot be Negative or 0");
            Customer_SSN = SSN;
            Sender_Address = sender;
            Receiver_Address = receiver;
            MyBox = x;
            Content_Isexpensive = isexpensive;
            Box_Weight = weight;
            Type = type;
            ID= ID_num+1;
            ID_num++;
            if (Phone_Num != "")
                Additional_Phone_Num = Phone_Num;

        }
        public static double Calculate_Price(Box_Content x, bool isexpensive, double weight, Post_Type type)
        {
            double Base_Price = 10;

            if (x == Box_Content.Object)
                Base_Price *= 1;
            else if (x == Box_Content.Document)
                Base_Price *= 1.5;
            else
                Base_Price *= 2;
            if (isexpensive)
                Base_Price *= 2;
            if(weight > 0.5)
            {
                double Diff = weight - 0.5;
                int Coefficient =(int)(Diff / 0.5);
                Base_Price *=Math.Pow(1.2,Coefficient);
            }
            if (type == Post_Type.Premium)
                Base_Price *= 2;
            
            return Base_Price;
        }
        public void Set_Status(Status New_Status)
        {
            if (This_Order_Status == Status.Received)
                throw new Exception("You cannot change the status of the order which has already been shipped");
            else
                This_Order_Status = New_Status;
        }
        
    }
}

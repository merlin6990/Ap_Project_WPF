using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
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
        public Order(string SSN,string sender,string receiver,Box_Content x,bool isexpensive,double weight,Post_Type type)
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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    
    public enum Month
    {
        January=0,
        February=1,
        March=2,
        April=3,
        May=4,
        June=5,
        July = 6,
        August = 7,
        September = 8,
        October = 9,
        November = 10,
        December = 11

    }
    public class Credit_Card
    {
        public static readonly int Current_Year = 1400;
        private string Card_Num;
        private string Cvv2;
        private Month Expiration_month;
        private int Expiration_Year;
        public Credit_Card(string Card_Number,string cvv2,Month month,int Year)
        {
            Card_Num = Card_Number;
            Cvv2 = cvv2;
            Expiration_month = month;
            Expiration_Year = Year;

        }
        // I nedd validity right Here LUHN

    }
}

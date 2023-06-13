using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DataAccess.Models
{
    public abstract class Validity
    {
        private static string Email_Validity_Pattern= @"^[a-zA-Z0-9\._-]{3,32}@[a-z]{3,32}.([a-z]{2,3}|com|net|org)$";
        private static string Name_Validity_Pattern= @"^[a-zA-Z]{3,32}$";
        private static string Phone_Number_Validity_Pattern = @"^09[0-9]{9}$";
        //Password is only for employees
        private static string Password_Validity_Pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$";
        //Password and username for customers is generated in customer constructor
        private static string CVV2_Validity_Pattern = @"^[0-9]{3,4}$";
        private static string SSN_Validity_Pattern = @"^00[0-9]{8}$";
        private static string ID_Validity_Pattern = @"^[0-9]{2}9[0-9]{2}$";

        //Here is all the Regexes that we need to check whether an Input is valid or not
        public static Regex Email_Validity  = new Regex(Email_Validity_Pattern);
        public static Regex Password_Validity= new Regex(Password_Validity_Pattern);
        public static Regex Name_Validity =new Regex(Name_Validity_Pattern);
        public static Regex Phone_Number_Validity = new Regex(Phone_Number_Validity_Pattern);
        public static Regex CVV2_Validity = new Regex(CVV2_Validity_Pattern);
        public static Regex SSN_Validity = new Regex(SSN_Validity_Pattern);
        public static Regex ID_Validity = new Regex(ID_Validity_Pattern);


        public static bool Email_Isvalid(string INPUT) { return Email_Validity.IsMatch(INPUT); }
        public static bool Name_Isvalid(string INPUT) { return Name_Validity.IsMatch(INPUT); }
        public static bool Phone_Number_Isvalid(string INPUT) { return Phone_Number_Validity.IsMatch(INPUT); }
        public static bool Password_Isvalid(string INPUT) { return Password_Validity.IsMatch(INPUT); }
        public static bool  CVV2_Isvalid(string INPUT) { return CVV2_Validity.IsMatch(INPUT);  }
        public static bool SSN_Isvalid(string INPUT) { return SSN_Validity.IsMatch(INPUT); }
        public static bool ID_Isvalid(string INPUT) { return ID_Validity.IsMatch(INPUT); }

    }
}

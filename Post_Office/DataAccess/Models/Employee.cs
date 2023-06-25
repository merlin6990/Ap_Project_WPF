using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Employee
    {
        private string First_Name="", Last_Name="", Email = "";
        private string ID = "",UserName = "",Password = "";
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
        public string _Last_Name { get { return Last_Name; }
            set
            {
                if (Validity.Name_Isvalid(value))
                    Last_Name = value;
                else
                    throw new Exception("Invalid pattern for LastName");

            }
        }
        public string _Email { 
            get { return Email; }
            set
            {
                if (Validity.Email_Isvalid(value))
                    Email = value;
                else
                    throw new Exception("Invalid pattern for email");
            }
        }
        public string _ID {
            get { return ID; }
            set
            {
                if (Validity.ID_Isvalid(value))
                    ID = value;
                else
                    throw new Exception("Invalid pattern for ID");
            }
        }
        public string _UserName { get { return UserName; }
            set
            {
                UserName = value;
            }
        }
        public string _Password { set
            {
                if (Validity.Password_Isvalid(value))
                    Password = value;
                else
                    throw new Exception("Invalid pattern for password");
            }
            get { return Password; }
        }
        
        public Employee(string first_Name, string last_Name, string email, string iD, string userName, string password)
        {
            _First_Name = first_Name;
            _Last_Name = last_Name;
            _Email = email;
            _ID = iD;
            _UserName = userName;
            _Password = password;
        }
    }
}

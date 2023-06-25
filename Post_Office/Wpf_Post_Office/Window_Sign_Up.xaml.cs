using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess;
using DataAccess.Models;

namespace Wpf_Post_Office
{
    /// <summary>
    /// Interaction logic for Window_Sign_Up.xaml
    /// </summary>
    public partial class Window_Sign_Up : Window
    {
        public Window_Sign_Up()
        {
            InitializeComponent();
        }

        private void sign_up_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Password1_Textbx.Text != Password2_Textbx.Text)
                    throw new Exception("Passwords don't Match");
                string First = First_Name_Textbx.Text;
                string Last = Last_Name_Textbx.Text;
                string id = Id_Textbx.Text;
                string username = User_Name_Textbx.Text;
                string email = Email_Textbx.Text;
                string password=Password1_Textbx.Text;
                Employee New_Emp = new Employee(First, Last, email, id, username, password);
                Data_Access_Unit.Add_New_Emp(New_Emp);
                MainWindow Emp_Panel=new MainWindow(New_Emp);
                Emp_Panel.Show();
                this.Close();
            }
            catch(Exception This)
            {
                MessageBox.Show(This.Message);
            }
        }
    }
}

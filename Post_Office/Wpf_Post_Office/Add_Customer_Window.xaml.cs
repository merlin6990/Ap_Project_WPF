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
    /// Interaction logic for Add_Customer_Window.xaml
    /// </summary>
    public partial class Add_Customer_Window : Window
    {
        public Add_Customer_Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Data_Access_Unit.Add_New_Customer(First_Name_Block.Text, Last_Name_Block.Text, SSN_Block.Text, Email_Block.Text, Phone_Number_Block.Text);
                this.Close();
            }
            catch(Exception This)
            {
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }
        }
    }
}

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
    /// Interaction logic for Add_Order_Window.xaml
    /// </summary>
    public partial class Add_Order_Window : Window
    {
        private Customer This_Customer;
        public Add_Order_Window(Customer x)
        {
            InitializeComponent();
            this.This_Customer = x;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sender_Address_Block.Text == "" || Receiver_Address_Block.Text == "")
                    throw new Exception("Address fields cannot be empty");
                if (Box_Type_Cbox.SelectedIndex == -1 || Post_Type_Cbox.SelectedIndex == -1)
                    throw new Exception("All of the combo Boxes must be selected");
                Box_Content This_Order_Type = (Box_Content)Box_Type_Cbox.SelectedIndex;
                bool IsExpensive = (bool)IS_Expensive_Checkbox.IsChecked;
                double Weight = double.Parse(Box_Weight_Block.Text);
                Post_Type This_Order_Post_Type = (Post_Type)Post_Type_Cbox.SelectedIndex;
                int Price = (int)Order.Calculate_Price(This_Order_Type, IsExpensive, Weight, This_Order_Post_Type);
                Register_Stack_Panel.Visibility = Visibility.Visible;
                Price_Tag.Content = Price.ToString()+"$";

            }
            catch(Exception This)
            {
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Box_Content This_Order_Type = (Box_Content)Box_Type_Cbox.SelectedIndex;
                bool IsExpensive = (bool)IS_Expensive_Checkbox.IsChecked;
                double Weight = double.Parse(Box_Weight_Block.Text);
                Post_Type This_Order_Post_Type = (Post_Type)Post_Type_Cbox.SelectedIndex;
                int Price = (int)Order.Calculate_Price(This_Order_Type, IsExpensive, Weight, This_Order_Post_Type);
                Data_Access_Unit.Add_New_Order(This_Customer._SSN, Sender_Address_Block.Text, Receiver_Address_Block.Text, This_Order_Type, IsExpensive, Weight, This_Order_Post_Type);
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

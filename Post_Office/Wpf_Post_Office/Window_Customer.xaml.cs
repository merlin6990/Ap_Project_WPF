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
    /// Interaction logic for Window_Customer.xaml
    /// </summary>
    public partial class Window_Customer : Window
    {
        Customer This_Customer;
        public Window_Customer(Customer X)
        {
            InitializeComponent();
/*            try
            {
                Data_Access_Unit.Read_From_DB();
            }
            catch(Exception This)
            {
                MessageBox.Show(This.Message);
            }*/
            This_Customer = X;
        }
        private void Home_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Visible;
            My_Orders_Panel.Visibility=Visibility.Collapsed;
            Order_Info_Panel.Visibility= Visibility.Collapsed;
            My_Wallet_Panel.Visibility=Visibility.Collapsed;
            Edit_Info_Panel.Visibility = Visibility.Collapsed;
        }

        private void My_Orders_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            My_Orders_Panel.Visibility = Visibility.Visible;
            Order_Info_Panel.Visibility = Visibility.Collapsed;
            My_Wallet_Panel.Visibility = Visibility.Collapsed;
            Edit_Info_Panel.Visibility = Visibility.Collapsed;
        }

        private void Order_Info_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            My_Orders_Panel.Visibility = Visibility.Collapsed;
            Order_Info_Panel.Visibility = Visibility.Visible;
            My_Wallet_Panel.Visibility = Visibility.Collapsed;
            Edit_Info_Panel.Visibility = Visibility.Collapsed;
        }

        private void My_Wallet_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            My_Orders_Panel.Visibility = Visibility.Collapsed;
            Order_Info_Panel.Visibility = Visibility.Collapsed;
            My_Wallet_Panel.Visibility = Visibility.Visible;
            Edit_Info_Panel.Visibility = Visibility.Collapsed;
        }

        private void Edit_Info_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            My_Orders_Panel.Visibility = Visibility.Collapsed;
            Order_Info_Panel.Visibility = Visibility.Collapsed;
            My_Wallet_Panel.Visibility = Visibility.Collapsed;
            Edit_Info_Panel.Visibility = Visibility.Visible;
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {

            Window_Log_In Login_Window = new Window_Log_In();
            Login_Window.Show();
            this.Close();
        }

        private void Advanced_Search_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string SSN = This_Customer._SSN;
                Box_Content Content = Box_Content.Object;
                int Price = 0;
                double Weight = 0;
                Post_Type Type = Post_Type.Normal;
                if ((bool)Use_Box_Type.IsChecked)
                {
                    if (Dummy_Box2.SelectedIndex == -1)
                        throw new Exception("When a parameter is checked the content cannot be empty");
                    Content = (Box_Content)Dummy_Box2.SelectedIndex;
                }
                if ((bool)Use_Price.IsChecked)
                {
                    if (int.Parse(Dummy_Box3.Text) <= 0)
                        throw new Exception("Price cannot be zero or negative");
                    Price = int.Parse(Dummy_Box3.Text);
                }
                if ((bool)Use_Weight.IsChecked)
                {
                    if (double.Parse(Dummy_Box4.Text) <= 0)
                        throw new Exception("Weight cannot be zero or negative");
                    Weight = double.Parse(Dummy_Box4.Text);
                }
                if ((bool)Use_Post_Type.IsChecked)
                {
                    if (Dummy_Box5.SelectedIndex == -1)
                        throw new Exception("When a parameter is checked the content cannot be empty");
                    Type = (Post_Type)Dummy_Box5.SelectedIndex;
                }

                var Result = Data_Access_Unit.Filtered_Orders(true, (bool)Use_Box_Type.IsChecked, (bool)Use_Price.IsChecked, (bool)Use_Weight.IsChecked, (bool)Use_Post_Type.IsChecked, SSN, Content, Price, Weight, Type);

                Search_Result.ItemsSource = Result;
                Search_Result.Visibility = Visibility.Visible;
            }
            catch (Exception This)
            {
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }
        }



        private void Use_Box_Type_Click(object sender, RoutedEventArgs e)
        {
            if (Dummy_Box2.Visibility == Visibility.Visible)
                Dummy_Box2.Visibility = Visibility.Hidden;
            else
                Dummy_Box2.Visibility = Visibility.Visible;
        }

        private void Use_Price_Click(object sender, RoutedEventArgs e)
        {
            if (Dummy_Box3.Visibility == Visibility.Visible)
                Dummy_Box3.Visibility = Visibility.Hidden;
            else
                Dummy_Box3.Visibility = Visibility.Visible;
        }

        private void Use_Weight_Click(object sender, RoutedEventArgs e)
        {
            if (Dummy_Box4.Visibility == Visibility.Visible)
                Dummy_Box4.Visibility = Visibility.Hidden;
            else
                Dummy_Box4.Visibility = Visibility.Visible;
        }

        private void Use_Post_Type_Click(object sender, RoutedEventArgs e)
        {
            if (Dummy_Box5.Visibility == Visibility.Visible)
                Dummy_Box5.Visibility = Visibility.Hidden;
            else
                Dummy_Box5.Visibility = Visibility.Visible;
        }

        private void See_Order_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Info = Data_Access_Unit.Show_Status(int.Parse(Search_Box_ID2.Text));
                var Info_List = new List<Order>();
                if (Info.SSN != This_Customer._SSN)
                    throw new Exception("The Order with this ID is not yours");
                Info_List.Add(Info);
                
                Not_Found_Label2.Visibility = Visibility.Hidden;
                Order_Info.ItemsSource = Info_List;
                Order_Info.Visibility = Visibility.Visible;

            }
            catch (Exception This)
            {
                Not_Found_Label2.Visibility = Visibility.Visible;
                Order_Info.Visibility = Visibility.Hidden;
            }
        }

        private void Clear_Page_btn_Click(object sender, RoutedEventArgs e)
        {
            Not_Found_Label2.Visibility = Visibility.Hidden;
            Order_Info.Visibility = Visibility.Hidden;
            Search_Box_ID2.Text = "";
        }

        private void Options_Page_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = Data_Access_Unit.Show_Status(int.Parse(Search_Box_ID2.Text));
                Customer_Comment Pivot_Wind = new Customer_Comment(order);
                Pivot_Wind.ShowDialog();
            }
            catch (Exception This)
            {
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }

        }

        private void Refresh_btn_Click(object sender, RoutedEventArgs e)
        {
            Balance_Label.Content=This_Customer.Get_My_Blance().ToString()+" $";
        }

        private void Charge_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data_Access_Unit.Charge_Account(This_Customer, Card_Number_Block.Text, Year_Number_Block.Text, Month_Number_Block.Text, CVV2_Number_Block.Text, Money_Number_Block.Text,true);
                Card_Number_Block.Text = "";
                Year_Number_Block.Text = "";
                Month_Number_Block.Text = "";
                CVV2_Number_Block.Text = "";
                Money_Number_Block.Text = "";
            }
            catch(Exception This)
            {
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data_Access_Unit.Change_Info_2(This_Customer,New_Username_Block.Text, New_Password_Block.Text);
                New_Username_Block.Text = "";
                New_Password_Block.Text = "";
                MessageBox.Show("Your Info was changed successfuly");
            }
            catch(Exception This)
            {
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }
        }
    }
}

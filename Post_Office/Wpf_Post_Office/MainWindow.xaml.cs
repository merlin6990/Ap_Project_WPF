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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccess;
using DataAccess.Models;

namespace Wpf_Post_Office
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        Employee This_Employee;
        public MainWindow(Employee X)
        {
            InitializeComponent();
            This_Employee= X;
            Customers_List.ItemsSource = Data_Access_Unit.Customer_Buffer;
        }

        private void Home_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Visible;
            Register_Customer_Panel.Visibility = Visibility.Collapsed;
            Register_Order_Panel.Visibility = Visibility.Collapsed;
            Order_List_Panel.Visibility = Visibility.Collapsed;
            Order_Info_Panel.Visibility = Visibility.Collapsed;
            Send_Email_Panel.Visibility = Visibility.Collapsed;
        }

        private void Register_Customer_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            Register_Customer_Panel.Visibility = Visibility.Visible;
            Register_Order_Panel.Visibility = Visibility.Collapsed;
            Order_List_Panel.Visibility = Visibility.Collapsed;
            Order_Info_Panel.Visibility = Visibility.Collapsed;
            Send_Email_Panel.Visibility = Visibility.Collapsed;
        }

        private void Register_Order_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            Register_Customer_Panel.Visibility = Visibility.Collapsed;
            Register_Order_Panel.Visibility = Visibility.Visible;
            Order_List_Panel.Visibility = Visibility.Collapsed;
            Order_Info_Panel.Visibility = Visibility.Collapsed;
            Send_Email_Panel.Visibility = Visibility.Collapsed;
        }

        private void Order_List_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            Register_Customer_Panel.Visibility = Visibility.Collapsed;
            Register_Order_Panel.Visibility = Visibility.Collapsed;
            Order_List_Panel.Visibility = Visibility.Visible;
            Order_Info_Panel.Visibility = Visibility.Collapsed;
            Send_Email_Panel.Visibility = Visibility.Collapsed;
        }

        private void Order_Info_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            Register_Customer_Panel.Visibility = Visibility.Collapsed;
            Register_Order_Panel.Visibility = Visibility.Collapsed;
            Order_List_Panel.Visibility = Visibility.Collapsed;
            Order_Info_Panel.Visibility = Visibility.Visible;
            Send_Email_Panel.Visibility = Visibility.Collapsed;

        }

        private void Send_Email_btn_Click(object sender, RoutedEventArgs e)
        {
            Home_Panel.Visibility = Visibility.Collapsed;
            Register_Customer_Panel.Visibility = Visibility.Collapsed;
            Register_Order_Panel.Visibility = Visibility.Collapsed;
            Order_List_Panel.Visibility = Visibility.Collapsed;
            Order_Info_Panel.Visibility = Visibility.Collapsed;
            Send_Email_Panel.Visibility = Visibility.Visible;
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            
            Window_Log_In Login_Window = new Window_Log_In();
            Login_Window.Show();
            this.Close();
        }

        private void Customers_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer This = (Customer)Customers_List.SelectedItem;

        }

        private void Add_Customer_btn_Click(object sender, RoutedEventArgs e)
        {
            Add_Customer_Window Temp_Wind=new Add_Customer_Window();
            Temp_Wind.ShowDialog();
            
        }

        private void Add_Order_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Searched_Customer=Data_Access_Unit.Search_Customer_ID(Search_Box_ID.Text);
                Not_Found_Label.Visibility = Visibility.Hidden;
                Add_Order_Window Temp_Wind = new Add_Order_Window(Searched_Customer);
                Temp_Wind.ShowDialog();

            }
            catch(Exception This)
            {
                //MessageBox.Show(This.Message);
                Not_Found_Label.Visibility = Visibility.Visible;

            }

        }

        private void Use_SSN_Click(object sender, RoutedEventArgs e)
        {
            if (Dummy_Box1.Visibility == Visibility.Visible)
                Dummy_Box1.Visibility = Visibility.Hidden;
            else
                Dummy_Box1.Visibility = Visibility.Visible;
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

        private void Advanced_Search_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string SSN = "";
                Box_Content Content = Box_Content.Object;
                int Price = 0;
                double Weight = 0;
                Post_Type Type = Post_Type.Normal;
                if ((bool)Use_SSN.IsChecked)
                {
                    if (Dummy_Box1.Text == "")
                        throw new Exception("When a parameter is checked the content cannot be empty");
                    SSN = Dummy_Box1.Text;
                }
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

                var Result = Data_Access_Unit.Filtered_Orders((bool)Use_SSN.IsChecked, (bool)Use_Box_Type.IsChecked, (bool)Use_Price.IsChecked, (bool)Use_Weight.IsChecked, (bool)Use_Post_Type.IsChecked, SSN, Content, Price, Weight, Type);

                Search_Result.ItemsSource = Result;
                Search_Result.Visibility = Visibility.Visible;
            }
            catch(Exception This)
            {
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }
        }

        private void See_Order_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Info = Data_Access_Unit.Show_Status(int.Parse(Search_Box_ID2.Text));
                var Info_List = new List<Order>();
                Info_List.Add(Info);
                Not_Found_Label2.Visibility = Visibility.Hidden;
                Order_Info.ItemsSource = Info_List;
                Order_Info.Visibility = Visibility.Visible;

            }
            catch(Exception This)
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
                Status_Comment Pivot_Wind = new Status_Comment(order);
                Pivot_Wind.ShowDialog();
            }
            catch(Exception This)
            {
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }

        }
    }
}

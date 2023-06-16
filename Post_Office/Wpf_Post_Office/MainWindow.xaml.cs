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
        public MainWindow()
        {
            InitializeComponent();
            Data_Access_Unit.Read_From_DB();
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
            //Going to The Log in Page NOT IMPLEMENTED YET
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
    }
}

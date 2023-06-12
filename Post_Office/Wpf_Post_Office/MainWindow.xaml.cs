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
    }
}

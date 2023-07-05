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
using DataAccess.Models;
using DataAccess;

namespace Wpf_Post_Office
{
    /// <summary>
    /// Interaction logic for Customer_Comment.xaml
    /// </summary>
    public partial class Customer_Comment : Window
    {
        Order This_Order;
        public Customer_Comment(Order x)
        {
            InitializeComponent();
            This_Order = x;
        }

        private void Send_btn_Click(object sender, RoutedEventArgs e)
        {
            /*            if(This_Order._Status==Status.Received)
                            This_Order.Customer_Comment = Search_Box_ID2.Text;
                        else
                        {
                            Send_btn.Visibility = Visibility.Hidden;
                            Search_Box_ID2.Visibility = Visibility.Hidden;
                        }*/

            try
            {
                if (Search_Box_ID2.Text == "")
                    throw new Exception("Comment cannot be empty");
                Data_Access_Unit.Set_Comment(This_Order, Search_Box_ID2.Text);
            }
            catch(Exception This)
            {
                MessageBox.Show(This.Message);
            }
        }
    }
}

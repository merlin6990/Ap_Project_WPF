using DataAccess.Models;
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

namespace Wpf_Post_Office
{
    /// <summary>
    /// Interaction logic for Status_Comment.xaml
    /// </summary>
    public partial class Status_Comment : Window
    {
        public Order This_Order_Point;
        public Status_Comment(Order This_Order)
        {
            InitializeComponent();
            if (This_Order.Customer_Comment != "")
            {
                This_Order_Point = This_Order;
                No_Comment_Label.Visibility = Visibility.Collapsed;
                Comment_Text.Text = This_Order.Customer_Comment;
                Comment_Text.Visibility = Visibility.Visible;
                if (This_Order._Status == Status.Received)
                {
                    Status_Type_Cbox.IsReadOnly = true;
                    Status_Type_Cbox.SelectedIndex = 3;
                    
                }
                else
                {
                    Status_Type_Cbox.SelectedIndex = (int)This_Order._Status;
                    
                }
            }
            Status_Label.Content += This_Order._Status.ToString();
            This_Order_Point = This_Order;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                This_Order_Point.Set_Status((Status)Status_Type_Cbox.SelectedIndex);
                if((Status)Status_Type_Cbox.SelectedIndex==Status.Received)
                    Status_Type_Cbox.IsReadOnly = true;
                Status_Label.Content = "Current Status :" + This_Order_Point._Status.ToString();

            }
            catch(Exception This)
            {
                Status_Type_Cbox.SelectedIndex = (int)This_Order_Point._Status;
                Error Temp_Error_Wind = new Error(This.Message);
                Temp_Error_Wind.ShowDialog();
            }
        }
    }
}

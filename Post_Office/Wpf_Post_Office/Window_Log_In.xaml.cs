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
    /// Interaction logic for Window_Log_In.xaml
    /// </summary>
    public partial class Window_Log_In : Window
    {
        public static int App_Awaken = 0;
        public Window_Log_In()
        {
            InitializeComponent();
            try
            {
                if (App_Awaken == 0)
                {
                    App_Awaken = 1;
                    Data_Access_Unit.Read_From_DB();
                }
            }
            catch(Exception This)
            {
                MessageBox.Show(This.Message);
            }
            
        }

        private void Log_In_btn_Click(object sender, RoutedEventArgs e)
        {
            string Username = User_Name_Textbx.Text;
            string Password = Password_Textbx.Text;
            //Function that goes through all of the users if it throws an exception there will be a pop up that pass or username is wrong otherwise we construct a window with that users object
            try
            {
                if (Data_Access_Unit.Log_IN_SearchC(Username, Password) != null)
                {
                    Window_Customer newwind = new Window_Customer(Data_Access_Unit.Log_IN_SearchC(Username, Password));
                    newwind.Show();
                    this.Close();
                }
                else if (Data_Access_Unit.Log_IN_SearchU(Username, Password) != null)
                {
                    MainWindow Newwind=new MainWindow(Data_Access_Unit.Log_IN_SearchU(Username, Password));
                    Newwind.Show();
                    this.Close();
                }
                else
                    throw new Exception("No user was found");
                Not_Found_Label.Visibility = Visibility.Hidden;
                this.Close();


            }
            catch(Exception This)
            {
                User_Name_Textbx.Text = "";
                Password_Textbx.Text = "";
                Not_Found_Label.Visibility = Visibility.Visible;
            }
        }

        private void Sign_Up_Link_Click(object sender, RoutedEventArgs e)
        {
            Window_Sign_Up Goto_Sign_Up=new Window_Sign_Up();
            Goto_Sign_Up.Show();
            this.Close();
        }
    }
}

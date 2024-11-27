using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System;


namespace BangumiMemoryManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Login_Button_Login_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                sql.Open();
                SqlCommand command = new SqlCommand("SELECT UserId FROM [User] WHERE UserName=@UserName AND UserPassword=@UserPassword", sql);
                command.Parameters.AddWithValue("@UserName", UserName_TextBox_Login.Text);
                command.Parameters.AddWithValue("@UserPassword", Password_PasswordBox_Login.Password);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) // 如果找到用户
                {
                    int userId = reader.GetInt32(0); // 获取 UserId
                    this.Hide();

                    UserPage userPage = new UserPage(userId); // 传递 UserId
                    userPage.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
        }

        private void Signup_Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (UserName_TextBox_Login.Text == "" || Password_PasswordBox_Login.Password == "")
            {
                MessageBox.Show("Please enter UserName and Password!");
            }
            else
            {
                using (SqlConnection sql = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    sql.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [User] WHERE UserName='" + UserName_TextBox_Login.Text + "'", sql);
                    DataTable UserDataTable = new DataTable();
                    sda.Fill(UserDataTable);
                    if (UserDataTable.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("UserName already exists!");
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO [User] (UserName, UserPassword) VALUES ('" + UserName_TextBox_Login.Text + "', '" + Password_PasswordBox_Login.Password + "')", sql);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("SignUp Successfully!");
                    }
                }
            }
        }

        private void Admin_Button_Login_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
        }
    }
}


/*
namespace BangumiMemoryManagementSystem
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
        //这里SqlConnection未找到可以查看 https://blog.csdn.net/weixin_40486955/article/details/103604422
        SqlConnection sql = new SqlConnection(DatabaseConfig.ConnectionString);
        

        private void Login_Button_Login_Click(object sender, RoutedEventArgs e)
        {
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT UserId FROM [User] WHERE UserName=@UserName AND UserPassword=@UserPassword", sql);
            command.Parameters.AddWithValue("@UserName", UserName_TextBox_Login.Text);
            command.Parameters.AddWithValue("@UserPassword", Password_PasswordBox_Login.Password);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read()) // 如果找到用户
            {
                int userId = reader.GetInt32(0); // 获取 UserId
                //MessageBox.Show("Login Successfully!");
                this.Hide();

                UserPage userPage = new UserPage(userId); // 传递 UserId
                userPage.Show();
            }
            else
            {
                MessageBox.Show("Login Failed!");
            }

            sql.Close();
        }


        private void Signup_Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if(UserName_TextBox_Login.Text == "" || Password_PasswordBox_Login.Password == "")
            {
                MessageBox.Show("Please enter UserName and Password!");
            }
            else
            {
                sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [User] WHERE UserName='" + UserName_TextBox_Login.Text + "'", sql);
                DataTable UserDataTable = new DataTable();
                sda.Fill(UserDataTable);
                if (UserDataTable.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("UserName already exists!");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [User] (UserName, UserPassword) VALUES ('" + UserName_TextBox_Login.Text + "', '" + Password_PasswordBox_Login.Password + "')", sql);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("SignUp Successfully!");
                }
                sql.Close();
            }

        }

        private void Admin_Button_Login_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
        }
    }
}
//此外提交git时有问题, 解决方法 https://blog.csdn.net/qq_40296909/article/details/134285451*/
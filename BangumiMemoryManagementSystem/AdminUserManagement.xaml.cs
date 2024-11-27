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
using static BangumiMemoryManagementSystem.UserPage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace BangumiMemoryManagementSystem
{
    public partial class AdminUserManagement : Window
    {
        public AdminUserManagement()
        {
            InitializeComponent();
            LoadData();
            Loaded += Window_Loaded;
        }

        public class User
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserPassword { get; set; }
        }

        public class DatabaseHelper
        {
            private string connectionString = DatabaseConfig.ConnectionString;

            public List<User> GetUsers()
            {
                var userList = new List<User>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM [User]", connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userList.Add(new User
                        {
                            UserId = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            UserPassword = reader.GetString(2)
                        });
                    }
                }
                return userList;
            }

            public void AddUser(User user)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO [User] (UserName, UserPassword) VALUES (@UserName, @UserPassword)", connection);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                    command.ExecuteNonQuery();
                }
            }

            public void DeleteUser(int userId)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM [User] WHERE UserId = @UserId", connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }

            public void UpdateUser(User user)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE [User] SET UserName = @UserName, UserPassword = @UserPassword WHERE UserId = @UserId", connection);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.ExecuteNonQuery();
                }
            }

            public List<User> SearchUsers(string searchTerm)
            {
                var userList = new List<User>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE UserName LIKE @SearchTerm", connection);
                    command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userList.Add(new User
                        {
                            UserId = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            UserPassword = reader.GetString(2)
                        });
                    }
                }
                return userList;
            }
        }

        private void LoadData()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<User> users = dbHelper.GetUsers();
            User_DataGrid_AdminUser.ItemsSource = users;
        }

        private void Exit_Button_AdminUserManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Add_Button_AdminUser_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                UserName = UserName_TextBox_AdminUser.Text,
                UserPassword = UserPassword_TextBox_AdminUser.Text
            };

            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.AddUser(newUser);
            LoadData();
            ClearTextBoxes();
        }

        private void Delete_Button_AdminUser_Click(object sender, RoutedEventArgs e)
        {
            if (User_DataGrid_AdminUser.SelectedItem is User selectedUser)
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                dbHelper.DeleteUser(selectedUser.UserId);
                LoadData();
            }
        }

        private void Save_Button_AdminUser_Click(object sender, RoutedEventArgs e)
        {
            if (User_DataGrid_AdminUser.SelectedItem is User selectedUser)
            {
                selectedUser.UserName = UserName_TextBox_AdminUser.Text;
                selectedUser.UserPassword = UserPassword_TextBox_AdminUser.Text;

                DatabaseHelper dbHelper = new DatabaseHelper();
                dbHelper.UpdateUser(selectedUser);
                LoadData();
                ClearTextBoxes();
            }
        }

        private void Search_Button_AdminUser_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = Search_TextBox_AdminUser.Text.Trim();
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<User> searchResults = dbHelper.SearchUsers(searchTerm);
            User_DataGrid_AdminUser.ItemsSource = searchResults;
        }

        private void User_DataGrid_AdminUser_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (User_DataGrid_AdminUser.SelectedItem is User selectedUser)
            {
                Id_TextBox_AdminUser.Text = selectedUser.UserId.ToString();
                UserName_TextBox_AdminUser.Text = selectedUser.UserName;
                UserPassword_TextBox_AdminUser.Text = selectedUser.UserPassword;
            }
        }

        private void ClearTextBoxes()
        {
            Id_TextBox_AdminUser.Clear();
            UserName_TextBox_AdminUser.Clear();
            UserPassword_TextBox_AdminUser.Clear();
            Search_TextBox_AdminUser.Clear();
        }

        private void Reset_Button_AdminUser_Click(object sender, RoutedEventArgs e)
        {
            LoadData(); // 重新加载所有用户数据
            ClearTextBoxes(); // 清空文本框
        }

        private void User_DataGrid_AdminUser_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (User_DataGrid_AdminUser.SelectedItem is User selectedUser)
            {
                // 填充文本框
                Id_TextBox_AdminUser.Text = selectedUser.UserId.ToString();
                UserName_TextBox_AdminUser.Text = selectedUser.UserName;
                UserPassword_TextBox_AdminUser.Text = selectedUser.UserPassword;
            }
        }

        private void Statistics_Button_AdminUser_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminStatistics adminStatistics = new AdminStatistics();
            adminStatistics.Show();
        }


        //随机Logo的图片
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string basePath = "pic/Firefly_Logo/Random/";
            string[] imageNames = {
                "firefly_Sonder/firefly_meme_0_Sonder",
                "firefly_Bangumi/firefly_meme_1_Bangumi",
                "firefly_Management/firefly_meme_2_Management",
                "firefly_System/firefly_meme_3_System"
            };
            RandomTopLogo.SetRandomImageSources(TopLogo_StackPanel_AdminUser, basePath, imageNames); // 注意这里使用 StackPanel
        }
    }
}

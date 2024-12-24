using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

//DatabaseConfig.ConnectionString


namespace BangumiMemoryManagementSystem
{
    /// <summary>
    /// UserPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserPage : Window
    {
        private int currentUserId; // 假设在登录时设置这个值

        public UserPage(int userId)
        {
            InitializeComponent();
            currentUserId = userId; // 设置当前用户的 UserId
            LoadData();
        }

        public class Bangumi
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public DateTime Time { get; set; }
            public int UserId { get; set; } // 新增 UserId 属性
        }

        public class DatabaseHelper
        {

            public List<Bangumi> GetBangumiData(int userId)
            {
                var bangumiList = new List<Bangumi>();

                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Bangumi WHERE UserId = @UserId", connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        bangumiList.Add(new Bangumi
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Time = reader.GetDateTime(3),
                            UserId = reader.GetInt32(4) // 读取 UserId
                        });
                    }
                }
                return bangumiList;
            }


            public void AddBangumi(Bangumi bangumi)
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Bangumi (Name, Type, Time, UserId) VALUES (@Name, @Type, @Time, @UserId)", connection);
                    command.Parameters.AddWithValue("@Name", bangumi.Name);
                    command.Parameters.AddWithValue("@Type", bangumi.Type);
                    command.Parameters.AddWithValue("@Time", bangumi.Time);
                    command.Parameters.AddWithValue("@UserId", bangumi.UserId); // 插入 UserId

                    command.ExecuteNonQuery();
                }
            }

            public void UpdateBangumi(int id, string name, string type, DateTime time)
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Bangumi SET Name = @Name, Type = @Type, Time = @Time WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@Time", time);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }

            public void DeleteBangumi(int id)
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Bangumi WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }

            public List<Bangumi> SearchBangumi(string searchTerm, int currentUserId)
            {
                var bangumiList = new List<Bangumi>();

                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Bangumi WHERE (Name LIKE @SearchTerm OR Type LIKE @SearchTerm) AND UserId = @UserId", connection);
                    command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    command.Parameters.AddWithValue("@UserId", currentUserId); // 加入 UserId 过滤条件
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        bangumiList.Add(new Bangumi
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Time = reader.GetDateTime(3),
                            UserId = reader.GetInt32(4) // 读取 UserId
                        });
                    }
                }

                return bangumiList;
            }
        }

        private void LoadData()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Bangumi> bangumiList = dbHelper.GetBangumiData(currentUserId); // 传递 UserId
            Bangumi_DataGrid_UserPage.ItemsSource = bangumiList;
        }

        private void Exit_Button_UserPage_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }


        private void Add_Button_UserPage_Click(object sender, RoutedEventArgs e)
        {
            // 获取用户输入
            string name = Name_TextBox_UserPage.Text;
            string type = Type_TextBox_UserPage.Text;
            DateTime time;

            // 验证时间输入
            if (!DateTime.TryParse(Time_TextBox_UserPage.Text, out time))
            {
                MessageBox.Show("请输入有效的时间格式。");
                return;
            }

            // 创建 Bangumi 对象
            Bangumi newBangumi = new Bangumi
            {
                Name = name,
                Type = type,
                Time = time,
                UserId = currentUserId // 设置当前用户的 UserId
            };

            // 添加到数据库
            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.AddBangumi(newBangumi);

            // 刷新 DataGrid 显示
            LoadData();

            // 清空输入框
            Name_TextBox_UserPage.Clear();
            Type_TextBox_UserPage.Clear();
            Time_TextBox_UserPage.Clear();
        }

        private void Bangumi_DataGrid_UserPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Bangumi_DataGrid_UserPage.SelectedItem is Bangumi selectedBangumi)
            {
                // 填充 TextBox
                //Id_TextBox_UserPage.Text = selectedBangumi.Id.ToString();
                Name_TextBox_UserPage.Text = selectedBangumi.Name;
                Type_TextBox_UserPage.Text = selectedBangumi.Type;
                //Time_TextBox_UserPage.Text = selectedBangumi.Time.ToString("yyyy-MM-dd HH:mm:ss"); // 格式化时间
                Time_TextBox_UserPage.Text = selectedBangumi.Time.ToString("yyyy MM dd"); // 格式化时间
            }
        }

        private void Save_Button_UserPage_Click(object sender, RoutedEventArgs e)
        {
            if (Bangumi_DataGrid_UserPage.SelectedItem is Bangumi selectedBangumi)
            {
                // 获取用户输入
                string name = Name_TextBox_UserPage.Text;
                string type = Type_TextBox_UserPage.Text;
                DateTime time;

                // 验证时间输入
                if (!DateTime.TryParse(Time_TextBox_UserPage.Text, out time))
                {
                    MessageBox.Show("请输入有效的时间格式。");
                    return;
                }

                // 更新到数据库
                DatabaseHelper dbHelper = new DatabaseHelper();
                dbHelper.UpdateBangumi(selectedBangumi.Id, name, type, time);

                // 刷新 DataGrid 显示
                LoadData();
            }
        }

        private void Delete_Button_UserPage_Click(object sender, RoutedEventArgs e)
        {
            if (Bangumi_DataGrid_UserPage.SelectedItem is Bangumi selectedBangumi)
            {
                // 确认删除
                MessageBoxResult result = MessageBox.Show("确定要删除该条目吗？", "确认删除", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    // 从数据库删除
                    DatabaseHelper dbHelper = new DatabaseHelper();
                    dbHelper.DeleteBangumi(selectedBangumi.Id);

                    // 刷新 DataGrid 显示
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("请先选择一条记录。");
            }
        }

        private void Search_Button_UserPage_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = Search_TextBox_UserPage.Text.Trim();

            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Bangumi> searchResults = dbHelper.SearchBangumi(searchTerm, currentUserId);

            Bangumi_DataGrid_UserPage.ItemsSource = searchResults;
        }

        private void ClearTextBoxes()
        {
            //Id_TextBox_UserPage.Clear();
            Name_TextBox_UserPage.Clear();
            Type_TextBox_UserPage.Clear();
            Time_TextBox_UserPage.Clear();
            Search_TextBox_UserPage.Clear(); // 清空搜索框
        }

        private void Reset_Button_UserPage_Click(object sender, RoutedEventArgs e)
        {
            LoadData(); // 重新加载原始数据
            ClearTextBoxes(); // 清空文本框
        }
    }
}

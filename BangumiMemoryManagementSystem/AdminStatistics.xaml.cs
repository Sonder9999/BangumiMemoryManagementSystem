using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Xml.Linq;

namespace BangumiMemoryManagementSystem
{
    /// <summary>
    /// AdminStatistics.xaml 的交互逻辑
    /// </summary>
    public partial class AdminStatistics : Window
    {
        private string connectionString = DatabaseConfig.ConnectionString;

        public AdminStatistics()
        {
            InitializeComponent();
            LoadStatistics();
            Loaded += Window_Loaded;
        }

        private void LoadStatistics()
        {
            UserAmount_Label_AdminStatistics.Content = GetUserCount().ToString();
            BangumiAmount_Label_AdminStatistics.Content = GetBangumiCount().ToString();
        }

        /*
         * 这里美化的话可以使用 https://honoka55.github.io/25ji-generator/ 里的logo生成器
         * 将UserCount传入到网站的数字框中,然后传入后面的"个" "," "用户"
         * 实现方法应该是调用网页的API,然后再通过网页的源代码Html找到对应的Text的框里来填入对应的元素
         * 最后生成图片,然后再将图片插入到这个窗口中
         * 下面的番剧数量也是一样的
         */
        private int GetUserCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [User]", connection);
                return (int)command.ExecuteScalar();
            }
        }

        private int GetBangumiCount()
        {
            //使用using可以自动释放资源
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Bangumi", connection);
                return (int)command.ExecuteScalar();
            }
        }

        /*     // 这是原来的写法，不推荐
         *      private int GetUserCount()
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [User]", connection);
                        return (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        // 可以在这里处理异常，例如记录日志
                        throw; // 重新抛出异常，或者根据需要处理
                    }
                    finally
                    {
                        // 确保在 finally 块中关闭连接
                        if (connection != null)
                        {
                            connection.Close();
                        }
                    }
                }
        */


        private void Exit_Button_AdminStatistics_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void User_Button_AdminStatistics_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminUserManagement adminUserManagement = new AdminUserManagement();
            adminUserManagement.Show();
        }
        private void ChangePassWord_Button_AdminStatistics_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                string ChangedPassword = ChangePassWord_TextBox_AdminStatistics.Text;
                if (ChangedPassword == "")
                {
                    MessageBox.Show("Please enter Password!");
                }
                else
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE [Admin] SET AdminPassword=@AdminPassword", connection);
                    command.Parameters.AddWithValue("@AdminPassword", ChangedPassword);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Change Password Successfully!");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string basePath = "pic/Firefly_Logo/Random/";
            string[] imageNames = {
                "firefly_Sonder/firefly_meme_0_Sonder",
                "firefly_Bangumi/firefly_meme_1_Bangumi",
                "firefly_Management/firefly_meme_2_Management",
                "firefly_System/firefly_meme_3_System"
            };
            RandomTopLogo.SetRandomImageSources(TopLogo_StackPanel_AdminStastics, basePath, imageNames); // 注意这里使用 StackPanel
        }


    }
}

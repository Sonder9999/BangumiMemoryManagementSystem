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
using System.Data;


namespace BangumiMemoryManagementSystem
{
    /// <summary>
    /// AdminLogin.xaml 的交互逻辑
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        SqlConnection sql = new SqlConnection(DatabaseConfig.ConnectionString);

        private void Login_Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (Password_PasswordBox_AdminLogin.Password == "")
            {
                MessageBox.Show("Please enter Password!");
            }
            else
            {
                sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [Admin] WHERE AdminPassword='" + Password_PasswordBox_AdminLogin.Password + "'", sql);
                DataTable AdminDataTable = new DataTable();
                sda.Fill(AdminDataTable);
                if (AdminDataTable.Rows[0][0].ToString() == "1")
                {
                    //MessageBox.Show("Login Successfully!");
                    this.Hide();
                    AdminUserManagement adminUserManagement = new AdminUserManagement();
                    adminUserManagement.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
                sql.Close();
            }
        }


        private void Exit_Button_UserPage_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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
            RandomTopLogo.SetRandomImageSources(TopLogo_StackPanel_AdminLogin, basePath, imageNames); // 注意这里使用 StackPanel
        }





        /*private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetRandomImageSource(TopLogo_01_Image_AdminLogin, "firefly_Sonder");
            SetRandomImageSource(TopLogo_02_Image_AdminLogin, "firefly_Bangumi");
            SetRandomImageSource(TopLogo_03_Image_AdminLogin, "firefly_Management");
            SetRandomImageSource(TopLogo_04_Image_AdminLogin, "firefly_System");
        }

        private void SetRandomImageSource(Image image, string folderName)
        {
            Random Random = new Random();
            int randomNumber = Random.Next(1, 9); // 生成 1 到 8 的随机数

            string imagePath = $"pic/Firefly_Logo/Random/{folderName}/firefly_meme_";

            // 根据文件夹名称构建正确的图像路径
            switch (folderName)
            {
                case "firefly_Sonder":
                    imagePath += $"0_Sonder_{randomNumber}.png";
                    break;
                case "firefly_Bangumi":
                    imagePath += $"1_Bangumi_{randomNumber}.png";
                    break;
                case "firefly_Management":
                    imagePath += $"2_Management_{randomNumber}.png";
                    break;
                case "firefly_System":
                    imagePath += $"3_System_{randomNumber}.png";
                    break;
                default: // 处理未知文件夹名称的默认情况
                    return; // 或者抛出异常
            }


            image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }*/
    }
}

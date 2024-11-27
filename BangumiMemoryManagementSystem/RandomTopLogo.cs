using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BangumiMemoryManagementSystem
{
    public static class RandomTopLogo // 静态类，无需实例化
    {
        private static Random random = new Random();

        /// <summary>
        /// 为一组图像设置随机源。
        /// </summary>
        /// <param name="images">包含要设置源的图像的 StackPanel 或其他容器。</param>
        /// <param name="basePath">图像的基本路径。</param>
        /// <param name="imageNames">图像名称数组（不包含扩展名）。</param>
        /// <param name="minRandom">随机数的最小值 (包含)。</param>
        /// <param name="maxRandom">随机数的最大值 (不包含)。</param>
        public static void SetRandomImageSources(Panel images, string basePath, string[] imageNames, int minRandom = 1, int maxRandom = 9)
        {
            // 确保图像名称数组和 images 容器中的图像数量匹配
            if (images.Children.Count != imageNames.Length)
            {
                // 处理错误，例如记录错误或抛出异常
                Console.WriteLine("Error: Image names array length does not match the number of images in the container.");
                return;
            }

            for (int i = 0; i < images.Children.Count; i++)
            {
                if (images.Children[i] is Image image)
                {
                    int randomNumber = random.Next(minRandom, maxRandom);
                    string imagePath = $"{basePath}{imageNames[i]}_{randomNumber}.png"; // 使用字符串插值构建路径
                    image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                }
            }
        }



        /// <summary>
        /// 为一组图像设置顺序源。
        /// </summary>
        /// <param name="images">包含要设置源的图像的StackPanel或其他容器。</param>
        /// <param name="basePath">图像的基本路径。</param>
        /// <param name="startIndex">起始索引。</param>
        /*
         * 如果是原来的这这种,会使用下面的重载函数
         *   <Viewbox Grid.Row="0" Grid.Column="1" Grid.ColumnSpan="1" Stretch="Uniform">
         *       <StackPanel Orientation="Horizontal">
         *           <Image Name="TopLogo_01_Image_AdminStastics" Source="pic/Firefly_Logo/firefly_0.png" MaxHeight="150"/>
         *           <Image Name="TopLogo_02_Image_AdminStastics" Source="pic/Firefly_Logo/firefly_1.png" MaxHeight="150"/>
         *           <Image Name="TopLogo_03_Image_AdminStastics" Source="pic/Firefly_Logo/firefly_2.png" MaxHeight="150"/>
         *           <Image Name="TopLogo_04_Image_AdminStastics" Source="pic/Firefly_Logo/firefly_3.png" MaxHeight="150"/>
         *       </StackPanel>
         *   </Viewbox>
         *   
         *   调用方法
         *  private void Window_Loaded(object sender, RoutedEventArgs e)
         *  {
         *    string basePath = "pic/Firefly_Logo/";
         *    RandomTopLogo.SetSequentialImageSources(TopLogo_StackPanel_AdminStatistics, basePath); // 假设 StackPanel 的名称为 TopLogo_StackPanel_AdminStatistics
         *  }
         */
        public static void SetSequentialImageSources(Panel images, string basePath, int startIndex = 0)
        {

            for (int i = 0; i < images.Children.Count; i++)
            {
                if (images.Children[i] is Image image)
                {

                    string imagePath = $"{basePath}firefly_{i + startIndex}.png"; // 使用字符串插值构建路径

                    image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                }
            }
        }

    }
}

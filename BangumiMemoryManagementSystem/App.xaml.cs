using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace BangumiMemoryManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MediaPlayer backgroundMusicPlayer;
        private List<string> musicFiles;
        private Random random;

        // 音乐文件夹路径
        //private readonly string musicFolderPath = @"C:/BangumiMemoryManagementSystem/BangumiMemoryManagementSystem/BackgroundMusic";
        private readonly string musicFolderPath = @"..\..\BackgroundMusic";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 初始化 MediaPlayer 和 Random 实例
            backgroundMusicPlayer = new MediaPlayer();
            random = new Random();

            // 获取音乐文件列表
            musicFiles = GetAllMusicFiles(musicFolderPath);

            // 检查是否有音乐文件
            if (musicFiles.Count > 0)
            {
                PlayRandomMusic();
            }

            // 监听 MediaEnded 事件，播放下一首随机歌曲
            backgroundMusicPlayer.MediaEnded += (sender, args) => PlayRandomMusic();
        }

        private List<string> GetAllMusicFiles(string folderPath)
        {
            // 获取所有 mp3 和 flac 文件（包括子文件夹）
            var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories).Where(file =>
            file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
            file.EndsWith(".flac", StringComparison.OrdinalIgnoreCase)
            ).ToList();
            return files;
        }

        private void PlayRandomMusic()
        {
            if (musicFiles.Count > 0)
            {
                // 随机选择一个音乐文件
                int index = random.Next(musicFiles.Count);
                string selectedMusic = musicFiles[index];

                try
                {
                    // 将相对路径转换为绝对路径
                    string absolutePath = Path.GetFullPath(selectedMusic);

                    // 打开并播放音乐
                    backgroundMusicPlayer.Open(new Uri(absolutePath));
                    backgroundMusicPlayer.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"播放音乐时出错: {ex.Message}");
                }
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // 停止音乐播放
            backgroundMusicPlayer?.Stop();
            base.OnExit(e);
        }

    }
}

/*
 // 暂停音乐
((App)Application.Current).backgroundMusicPlayer.Pause();

// 继续播放
((App)Application.Current).backgroundMusicPlayer.Play();
 */
/*
 using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace BangumiMemoryManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MediaPlayer backgroundMusicPlayer;
        private List<string> musicFiles;
        private Random random;

        // 相对音乐文件夹路径
        private readonly string relativeMusicFolderPath = @"BackgroundMusic";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 初始化 MediaPlayer 和 Random 实例
            backgroundMusicPlayer = new MediaPlayer();
            random = new Random();

            // 获取音乐文件夹的完整路径
            string musicFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeMusicFolderPath);

            // 获取音乐文件列表
            musicFiles = GetAllMusicFiles(musicFolderPath);

            // 检查是否有音乐文件
            if (musicFiles.Count > 0)
            {
                PlayRandomMusic();
            }

            // 监听 MediaEnded 事件，播放下一首随机歌曲
            backgroundMusicPlayer.MediaEnded += (sender, args) => PlayRandomMusic();
        }

        private List<string> GetAllMusicFiles(string folderPath)
        {
            try
            {
                // 获取所有 mp3 和 flac 文件（包括子文件夹）
                return Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                    .Where(file =>
                        file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
                        file.EndsWith(".flac", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show($"未找到音乐文件夹: {folderPath}");
                return new List<string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取音乐文件时出错: {ex.Message}");
                return new List<string>();
            }
        }

        private void PlayRandomMusic()
        {
            if (musicFiles.Count > 0)
            {
                // 随机选择一个音乐文件
                int index = random.Next(musicFiles.Count);
                string selectedMusic = musicFiles[index];

                try
                {
                    // 打开并播放音乐
                    backgroundMusicPlayer.Open(new Uri(selectedMusic));
                    backgroundMusicPlayer.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"播放音乐时出错: {ex.Message}");
                }
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // 停止音乐播放
            backgroundMusicPlayer?.Stop();
            base.OnExit(e);
        }
    }
}

*/
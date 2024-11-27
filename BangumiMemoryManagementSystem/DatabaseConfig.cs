using System;
using System.IO;

namespace BangumiMemoryManagementSystem
{
    public static class DatabaseConfig
    {
        // 静态属性，用于获取数据库连接字符串
        public static string ConnectionString
        {
            get
            {
                //C:\BangumiMemoryManagementSystem\BangumiMemoryManagementSystem\Sql\BangumiMemoryManagementSystem.mdf
                // 动态获取运行目录
                string runDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // 拼接数据库文件路径
                string relativePath = @"..\..\Sql\BangumiMemoryManagementSystem.mdf";
                string databasePath = Path.GetFullPath(Path.Combine(runDirectory, relativePath));

                // 返回完整连接字符串
                return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databasePath};Integrated Security=True;Connect Timeout=30";
            }
        }
    }
}

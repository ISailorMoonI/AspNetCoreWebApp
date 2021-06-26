using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreWebApp
{
    class Program
    {
        public static string FilePath = @"C:\Users\User\Posts.txt";
        public static readonly int FromId = 4;
        public static readonly int ToId = 13;

        static async Task Main(string[] args)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            var countIds = Math.Abs(ToId - FromId) + 1;
            var ids = Enumerable.Range(Math.Min(FromId, ToId), countIds);
            var posts = await Service.GetPosts(ids.ToArray());

            Console.WriteLine($"Posts, Count:{posts.Count}");
            Console.WriteLine($"Posts Ids where wrote to file:");

            foreach (var post in posts)
            {
                post.WriteToFileAsync(FilePath);
                Console.WriteLine("Done");
            }

            try
            {
                Process.Start("notepad.exe", FilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't open the file ({FilePath})");
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreWebApp
{
    public static class Extension
    {
        public static async void WriteToFileAsync(this Post post, string filePath)
        {
            var textLines = new string[]
            {
                $"{post.UserId}",
                $"{post.Id}",
                post.Title,
                post.Body,
                Environment.NewLine
            };
            await File.AppendAllLinesAsync(filePath, textLines);
        }
    }
}

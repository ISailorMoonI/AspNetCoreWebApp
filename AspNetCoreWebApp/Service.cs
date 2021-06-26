using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetCoreWebApp
{
    public static class Service
    {
        private const string GetPostRequestUri = "https://jsonplaceholder.typicode.com/posts/";


        public static async Task<Post> GetPost(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync($"{GetPostRequestUri}{id}");
                var result = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var post = JsonSerializer.Deserialize<Post>(result, options);
                return post;
            }
            
            catch
            {
                return null;
            }
        }

        public static async Task<List<Post>> GetPosts(params int[] ids)
        {
            var tasks = new List<Task<Post>>();
            var posts = new List<Post>();
            for (int i = 0; i < ids.Length; i++)
            {
                tasks.Add(GetPost(ids[i]));
            }

            await Task.WhenAll(tasks);

            posts = tasks
                .Select(t => t.Result)
                .Where(p => p != null)
                .Where(p => p.Id > 0)
                .ToList();

            return posts;

        }
    }
}

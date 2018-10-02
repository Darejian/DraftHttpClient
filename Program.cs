using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace WebAPIClient
{
    partial class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            //ProcessRepositories().Wait();
            var repositories = ProcessRepositories().Result;


            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitHubHomeUrl);
                Console.WriteLine(repo.Homepage);
                Console.WriteLine(repo.Watchers);
                Console.WriteLine(repo.LastPush);
                Console.WriteLine();
            }
            Console.Read();
        }
 

        private static async Task<List<Repository>> ProcessRepositories()
        //private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));
            //var serializer = new DataContractJsonSerializer(typeof(List<repo>));
            //var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var repositories = serializer.ReadObject(await streamTask) as List<Repository>;
            return repositories;
            //var repositories = serializer.ReadObject(await streamTask) as List<Repository>;
            // var repositories = serializer.ReadObject(await streamTask) as List<repo>;

            /*var msg = await stringTask;
            Console.Write(msg);
            */

            /*foreach (var Repository in repositories)
                Console.WriteLine(Repository.Name);
            Console.Read();
            */
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using AngleSharp.Parser.Html;
using Jokify.Data;
using Jokify.Data.Common;
using Jokify.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jokify.WebCrawler
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;
                WebCrawlerCode(serviceProvider);
            }
        }

        private static void WebCrawlerCode(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<JokifyContext>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var parser = new HtmlParser();
            var webClient = new WebClient {Encoding = Encoding.GetEncoding("windows-1251") };

            for (var i = 1; i < 10000; i++)
            {
                var url = "http://fun.dir.bg/vic_open.php?id=" + i;
                string html = null;
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        html = webClient.DownloadString(url);
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(10000);
                    }
                }

                if (string.IsNullOrWhiteSpace(html))
                {
                    continue;
                }

                var document = parser.Parse(html);
                var jokeContent = document.QuerySelector("#newsbody")?.TextContent?.Trim();
                var categoryName = document.QuerySelector(".tag-links-left a")?.TextContent?.Trim();

                if (!string.IsNullOrWhiteSpace(jokeContent) &&
                    !string.IsNullOrWhiteSpace(categoryName))
                {
                    var category = context.Categories.FirstOrDefault(x => x.Name == categoryName);
                    if (category == null)
                    {
                        category = new Category
                        {
                            Name = categoryName,
                        };
                    }

                    var joke = new Joke()
                    {
                        Category = category,
                        Content = jokeContent,
                    };

                    context.Jokes.Add(joke);
                    context.SaveChanges();
                }

                Console.WriteLine($"{i} => {categoryName}");
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddDbContext<JokifyContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
        }
    }
}

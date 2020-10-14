using System;
using System.Threading.Tasks;
using BookServiceClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookServiceClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Client app, wait for service");
            Console.ReadLine();
            ConfgureService();
            var test = ApplicationService.GetService<SampleRequest>();
            await test.ReadChaptersAsync();

            //Console.WriteLine("enter the BookChapter Id:");
            //await test.ReadChapterAsync(Console.ReadLine());

            //await test.AddChapterAsync();
            //await test.UpdateChapterAsync();
            //await test.RemoveChapterAsync();
            //await test.ReadXmlAsync();
            //await test.ReadNotExistingChapterAsync();
        }
        static void ConfgureService()
        {
            var services = new ServiceCollection();
            services.AddSingleton<UrlService>();
            services.AddSingleton<BookChapterClientService>();
            services.AddSingleton<SampleRequest>();
            services.AddLogging(logger=>
            logger.AddConsole());
            ApplicationService = services.BuildServiceProvider();
        }
        public static IServiceProvider ApplicationService { get; set; }
    }
}

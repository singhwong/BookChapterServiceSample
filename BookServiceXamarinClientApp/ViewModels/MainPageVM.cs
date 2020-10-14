using System;
using System.Diagnostics;
using System.Windows.Input;
using BookServiceXamarinClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.ViewModels
{
    public class MainPageVM
    {
        public MainPageVM()
        {
            NavigationCommand = new Command<Type>(async(Type type)=>
            {
                Page page = (Page)Activator.CreateInstance(type);
                await Application.Current.MainPage.Navigation.PushAsync(page);
                //var request = ApplicationService.GetService<RequestService>();
                //var list = await request.ReadChaptersAsync();
                //foreach (var item in list)
                //{
                //    Debug.WriteLine(item.Title);
                //}
            });
            ConfigureService();
        }
        public ICommand NavigationCommand { get; set; }
        public static void ConfigureService()
        {
            var services = new ServiceCollection();
            services.AddSingleton<UrlService>();
            services.AddSingleton<BookChapterClientService>();
            services.AddSingleton<RequestService>();
            services.AddLogging(logger =>
            logger.AddConsole());
            ApplicationService = services.BuildServiceProvider();
        }
        public static IServiceProvider ApplicationService { get; set; }
    }
}

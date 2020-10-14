using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using BookServiceXamarinClientApp.Models;
using BookServiceXamarinClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BookServiceXamarinClientApp.ViewModels
{
    public class AllChaptersVM
    {
        public AllChaptersVM()
        {
            var request = MainPageVM.ApplicationService.GetService<RequestService>();
            GetAllChaptersCommand = new Command(async () =>
            {
                var chapters = await request.ReadChaptersAsync();
                chapters.ForEach(c => AllChapters.Add(c));
                //Debug.WriteLine(chapters.Count());
            });
        }
        public ObservableCollection<BookChapter> AllChapters { get; } = new ObservableCollection<BookChapter>();
        public ICommand GetAllChaptersCommand { get; }
    }
}

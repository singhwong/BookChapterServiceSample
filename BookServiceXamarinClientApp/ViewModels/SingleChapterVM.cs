using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using BookServiceXamarinClientApp.Frameworks;
using BookServiceXamarinClientApp.Models;
using BookServiceXamarinClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.ViewModels
{
    public class SingleChapterVM:BindableBase
    {
        public SingleChapterVM()
        {
            var request = MainPageVM.ApplicationService.GetService<RequestService>();
            GetSingleCommand = new Command(async()=>
            {
                if (string.IsNullOrEmpty(EnteredId))
                {
                    return;
                }
                try
                {
                    BookChapterResult = await request.ReadChapterAsync(EnteredId);
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("404"))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Id not fond!", "OK");
                    BookChapterResult = new BookChapter
                    {
                        Id = Guid.Empty,
                        Title = "null",
                        Number = 0,
                        PublisherNumber = 0
                    };
                }
                Debug.WriteLine(BookChapterResult.Title);
            });
        }
        public string EnteredId { get; set; }
        private BookChapter _bookChapterResult;
        public BookChapter BookChapterResult
        {
            get => _bookChapterResult;
            set => Set(ref _bookChapterResult,value);
        }
        public ICommand GetSingleCommand { get; set; }
    }
}

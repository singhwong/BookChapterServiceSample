using System;
using System.Net.Http;
using System.Windows.Input;
using BookServiceXamarinClientApp.Models;
using BookServiceXamarinClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.ViewModels
{
    public class PutChapterVM
    {
        public PutChapterVM()
        {
            var request = MainPageVM.ApplicationService.GetService<RequestService>();
            PutChapterCommand = new Command(async()=>
            {
                var chapter = SetChapterValue();
                try
                {
                    await request.UpdateChapterAsync(chapter);
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("404"))
                {
                    await Application.Current.MainPage.DisplayAlert("alert","Not found! (404)","OK");
                }
            });

        }
        private BookChapter SetChapterValue()
        {
            if (Guid.TryParse(EnteredId, out var id))
                BookChapterResult.Id = id;
            if (!string.IsNullOrEmpty(EnteredTitle))
                BookChapterResult.Title = EnteredTitle;
            if (int.TryParse(EnteredNumber, out var number))
                BookChapterResult.Number = number;
            if (int.TryParse(EnteredPublisherNumber, out var publisherNunber))
                BookChapterResult.PublisherNumber = publisherNunber;
            return BookChapterResult;
        }
        public string EnteredId { get; set; }
        public string EnteredTitle { get; set; }
        public string EnteredNumber { get; set; }
        public string EnteredPublisherNumber { get; set; }
        private BookChapter BookChapterResult = new BookChapter();
        public ICommand PutChapterCommand { get; set; }
    }
}

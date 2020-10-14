using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BookServiceXamarinClientApp.Frameworks;
using BookServiceXamarinClientApp.Models;
using BookServiceXamarinClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.ViewModels
{
    public class PostChapterVM:BindableBase
    {
        public PostChapterVM()
        {

            var request = MainPageVM.ApplicationService.GetService<RequestService>();
            PostChapterCommand = new Command(async()=>
            {
               var chapter = await request.AddChapterAsync( await GetChapterValue());
                BookChapterResult = chapter;
            });
        }
        public async Task<BookChapter> GetChapterValue()
        {
            if (!string.IsNullOrEmpty(EnteredTitle))
                BookChapterResult.Title = EnteredTitle;
            else
                await DisplayAlert(nameof(EnteredTitle));
            if (int.TryParse(EnteredNumber, out var number))
                BookChapterResult.Number = number;
            else
                await DisplayAlert(nameof(EnteredNumber));
            if (int.TryParse(EnteredPublisherNumber, out var publisherNumber))
                BookChapterResult.PublisherNumber = publisherNumber;
            else
                await DisplayAlert(nameof(EnteredPublisherNumber));
            return BookChapterResult;
        }
        public async Task DisplayAlert(string propertyName)
        {
            await Application.Current.MainPage.DisplayAlert("alert",$"{propertyName} value invalid","OK");
        }
        public string EnteredTitle { get; set; }
        public string EnteredNumber { get; set; }
        public string EnteredPublisherNumber { get; set; }
        public ICommand PostChapterCommand { get; set; }
        private BookChapter _bookChapterResult = new BookChapter();
        public BookChapter BookChapterResult
        {
            get => _bookChapterResult;
            set => Set(ref _bookChapterResult,value);
        }
    }
}

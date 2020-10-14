using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookServiceXamarinClientApp.Models;

namespace BookServiceXamarinClientApp.Services
{
    public class RequestService
    {
        private readonly UrlService _urlService;
        private BookChapterClientService _bookChapterClientService;
        public RequestService(UrlService urlService, BookChapterClientService bookChapterClientService)
        {
            _urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
            _bookChapterClientService = bookChapterClientService ?? throw new ArgumentNullException(nameof(bookChapterClientService));
        }
        public async Task<IEnumerable<BookChapter>> ReadChaptersAsync()
        {
            //Console.WriteLine(nameof(ReadChapterAsync));
            IEnumerable<BookChapter> chapters = await _bookChapterClientService.GetAllAsync(_urlService.BookApi);
            //foreach (var chapter in chapters)
            //{
            //    Console.WriteLine($"{chapter.Id} {chapter.Title}");
            //}
            return chapters;
        }
        public async Task ReadNotExistingChapterAsync()
        {
            //Console.WriteLine(nameof(ReadNotExistingChapterAsync));
            Guid requestedIdentifier = Guid.NewGuid();
            try
            {
                var chapter = await _bookChapterClientService.GetAsync(_urlService.BookApi + requestedIdentifier);
                Debug.WriteLine($"{chapter.Id} {chapter.Title}");
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("404"))
            {
                Debug.WriteLine($"book chapter with the identifier {requestedIdentifier} not found.");
            }
        }
        public async Task<BookChapter> ReadChapterAsync(string Id)
        {
            //Console.WriteLine(nameof(ReadChapterAsync));
            //var chapters = await _bookChapterClientService.GetAllAsync(_urlService.BookApi);
            //Guid id = chapters.First().Id;
            var chapter = await _bookChapterClientService.GetAsync(_urlService.BookApi + Id);
            //Console.WriteLine($"{chapter.Id} {chapter.Title} {chapter.Number} {chapter.PublisherNumber}");
            return chapter;
        }
        public async Task<BookChapter> AddChapterAsync(BookChapter chapter)
        {
            //Console.WriteLine(nameof(AddChapterAsync));
            //BookChapter chapter = new BookChapter
            //{
            //    //Id = Guid.NewGuid(), //id可以直接在服务器中填充
            //    Number = 34,
            //    Title = "ASP.NET Core Web API",
            //    PublisherNumber = 35
            //};
            return  await _bookChapterClientService.PostAsync(_urlService.BookApi, chapter);
            //Console.WriteLine($"added chapter {chapter.Id} {chapter.Title}");
        }
        public async Task UpdateChapterAsync(BookChapter chapter)
        {
            //Console.WriteLine(nameof(UpdateChapterAsync));
            //var chapters = await _bookChapterClientService.GetAllAsync(_urlService.BookApi);
            //var chapter = chapters.SingleOrDefault(c => c.Title == "WXG");
            //if (chapter != null)
            //{
            //    chapter.Title = "WXG! Hello World!";
                await _bookChapterClientService.PutAsync(_urlService.BookApi + chapter.Id, chapter);
                Debug.WriteLine($"updated chapter {chapter.Title}");
            //}
        }
        public async Task RemoveChapterAsync(Guid id)
        {
            //Console.WriteLine(nameof(RemoveChapterAsync));
            //var chapters = await _bookChapterClientService.GetAllAsync(_urlService.BookApi);
            //var chapter = chapters.SingleOrDefault(c => c.Title == "WXG");
            //if (chapter != null)
            //{
                await _bookChapterClientService.DeleteAsync(_urlService.BookApi + id);
                //Debug.WriteLine($"removed chapter {chapter.Title}");
            //}
        }
        public async Task ReadXmlAsync()
        {
            //Console.WriteLine(nameof(ReadXmlAsync));
            var chapters = await _bookChapterClientService.GetAllXmlAsync(_urlService.BookApi);
            Debug.WriteLine(chapters);
        }
    }
}

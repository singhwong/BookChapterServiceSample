using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookServiceClientApp.Models;
using BookServiceClientApp.Services;

namespace BookServiceClientApp
{
    public class SampleRequest
    {
        private readonly UrlService _urlService;
        private BookChapterClientService _bookChapterClientService;
        public SampleRequest(UrlService urlService,BookChapterClientService bookChapterClientService)
        {
            _urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
            _bookChapterClientService = bookChapterClientService ?? throw new ArgumentNullException(nameof(bookChapterClientService));
        }
        public async Task ReadChaptersAsync()
        {
            Console.WriteLine(nameof(ReadChaptersAsync));
            IEnumerable<BookChapter> chapters = await _bookChapterClientService.GetAllAsync(_urlService.BookApi);
            foreach (var chapter in chapters)
            {
                Console.WriteLine($"id: {chapter.Id} title: {chapter.Title} publisher number: {chapter.PublisherNumber} number: {chapter.Number}");
            }
        }
        public async Task ReadNotExistingChapterAsync()
        {
            Console.WriteLine(nameof(ReadNotExistingChapterAsync));
            Guid requestedIdentifier = Guid.NewGuid();
            try
            {
                var chapter = await _bookChapterClientService.GetAsync(_urlService.BookApi + requestedIdentifier);
                Console.WriteLine($"{chapter.Id} {chapter.Title}");
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("404"))
            {
                Console.WriteLine($"book chapter with the identifier {requestedIdentifier} not found.");
            }
        }
        public async Task ReadChapterAsync(string Id)
        {
            Console.WriteLine(nameof(ReadChapterAsync));
            //var chapters = await _bookChapterClientService.GetAllAsync(_urlService.BookApi);
            //Guid id = chapters.First().Id;
            var chapter = await _bookChapterClientService.GetAsync(_urlService.BookApi + Id);
            Console.WriteLine($"{chapter.Id} {chapter.Title} {chapter.Number} {chapter.PublisherNumber}");
        }
        public async Task AddChapterAsync()
        {
            Console.WriteLine(nameof(AddChapterAsync));
            BookChapter chapter = new BookChapter
            {
                //Id = Guid.NewGuid(), //id可以直接在服务器中填充
                Number = 34,
                Title = "ASP.NET Core Web API",
                PublisherNumber = 35
            };
            chapter = await _bookChapterClientService.PostAsync(_urlService.BookApi, chapter);
            Console.WriteLine($"added chapter {chapter.Id} {chapter.Title}");
        }
        public async Task UpdateChapterAsync()
        {
            Console.WriteLine(nameof(UpdateChapterAsync));
            var chapters = await _bookChapterClientService.GetAllAsync(_urlService.BookApi);
            var chapter = chapters.SingleOrDefault(c => c.Title == "WXG");
            if (chapter != null)
            {
                chapter.Title = "WXG! Hello World!";
                await _bookChapterClientService.PutAsync(_urlService.BookApi + chapter.Id, chapter);
                Console.WriteLine($"updated chapter {chapter.Title}");
            }
        }
        public async Task RemoveChapterAsync()
        {
            Console.WriteLine(nameof(RemoveChapterAsync));
            var chapters = await _bookChapterClientService.GetAllAsync(_urlService.BookApi);
            var chapter = chapters.SingleOrDefault(c => c.Title == "WXG");
            if (chapter != null)
            {
                await _bookChapterClientService.DeleteAsync(_urlService.BookApi + chapter.Id);
                Console.WriteLine($"removed chapter {chapter.Title}");
            }
        }
        public async Task ReadXmlAsync()
        {
            Console.WriteLine(nameof(ReadXmlAsync));
            var chapters =  await _bookChapterClientService.GetAllXmlAsync(_urlService.BookApi);
            Console.WriteLine(chapters);
        }
    }
}

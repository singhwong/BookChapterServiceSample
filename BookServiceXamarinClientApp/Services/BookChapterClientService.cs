using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookServiceXamarinClientApp.Models;
using Microsoft.Extensions.Logging;

namespace BookServiceXamarinClientApp.Services
{
    public class BookChapterClientService : HttpClientService<BookChapter>
    {
        public BookChapterClientService(UrlService urlService, ILogger<BookChapterClientService> logger)
            : base(urlService, logger) { }
        public override async Task<IEnumerable<BookChapter>> GetAllAsync(string requestUri)
        {
            IEnumerable<BookChapter> chapters = await base.GetAllAsync(requestUri);
            return chapters.OrderBy(c => c.Title);
        }
        public async override Task<BookChapter> GetAsync(string requestUri)
        {
            return await base.GetAsync(requestUri);
        }
        public async override Task<BookChapter> PostAsync(string requestUri, BookChapter chapter)
        {
            return await base.PostAsync(requestUri, chapter);
        }
        public async override Task PutAsync(string requestUri, BookChapter chapter)
        {
            await base.PutAsync(requestUri, chapter);
        }
    }
}

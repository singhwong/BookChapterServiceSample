using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookServiceClientApp.Models;
using Microsoft.Extensions.Logging;

namespace BookServiceClientApp.Services
{
    public class BookChapterClientService:HttpClientService<BookChapter>
    {
        public BookChapterClientService(UrlService urlService, ILogger<BookChapterClientService> logger)
            : base(urlService, logger) { }
        public override async Task<IEnumerable<BookChapter>> GetAllAsync(string requestUri)
        {
            IEnumerable<BookChapter> chapters = await base.GetAllAsync(requestUri);
            chapters.OrderBy(c => c.Id);
            return chapters;
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

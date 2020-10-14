using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksServiceSampleHost.Models;
using Microsoft.Extensions.Logging;

namespace BooksServiceSampleHost.Services
{
    public class BookChaptersService:IBookChaptersService
    {
        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters =
        new ConcurrentDictionary<Guid, BookChapter>();
        public Task AddAsync(BookChapter bookChapter)
        {
            bookChapter.Id = Guid.NewGuid();
            _chapters[bookChapter.Id] = bookChapter;
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            foreach (var chapter in chapters)
            {
                chapter.Id = Guid.NewGuid();
                _chapters[chapter.Id] = chapter;
            }
            return Task.CompletedTask;
        }

        public Task<BookChapter> FindAsync(Guid id)
        {
            _chapters.TryGetValue(id, out BookChapter bookChapter);
            return Task.FromResult<BookChapter>(bookChapter);
        }

        public Task<IEnumerable<BookChapter>> GetAllAsync() => Task.FromResult<IEnumerable<BookChapter>>(_chapters.Values);

        public Task<BookChapter> RemoveAsync(Guid id)
        {
            _chapters.TryRemove(id, out BookChapter remove);
            return Task.FromResult<BookChapter>(remove);
        }

        public Task UpdateAsync(BookChapter bookChapter)
        {
            _chapters[bookChapter.Id] = bookChapter;
            return Task.CompletedTask;
        }
    }
}

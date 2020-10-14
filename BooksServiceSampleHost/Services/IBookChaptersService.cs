using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksServiceSampleHost.Models;

namespace BooksServiceSampleHost.Services
{
    public interface IBookChaptersService
    {
        Task AddAsync(BookChapter bookChapter);
        Task AddRangeAsync(IEnumerable<BookChapter> chanters);
        Task<IEnumerable<BookChapter>> GetAllAsync();
        Task<BookChapter> FindAsync(Guid id);
        Task<BookChapter> RemoveAsync(Guid id);
        Task UpdateAsync(BookChapter bookChapter);
    }
}

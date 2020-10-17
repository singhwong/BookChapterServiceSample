using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksServiceSampleHost.Models;
using Microsoft.Extensions.Logging;
using BooksServiceSampleHost.DataBaseConf;
using SQLite;
using System.Diagnostics;
using System.IO;

namespace BooksServiceSampleHost.Services
{
    public class BookChaptersService:IBookChaptersService
    {
        private SampleChapters _sampleChapters;
        public BookChaptersService(SampleChapters sampleChapters)
        {
            _sampleChapters = sampleChapters;
            InitializeAsync().SafeFireAndForget(false);
        }
        //private readonly ConcurrentDictionary<Guid, BookChapter> _chapters =
        //new ConcurrentDictionary<Guid, BookChapter>();
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            Debug.WriteLine(Contants.DatabasePath);
            return new SQLiteAsyncConnection(Contants.DatabasePath, Contants.Flags);
        });
        static SQLiteAsyncConnection DatabaseConnection => lazyInitializer.Value;
        static bool initialized = false;
        async Task InitializeAsync()
        {
            if (!initialized)
            {

                if (!File.Exists(Contants.DatabasePath))
                { 
                    await DatabaseConnection.CreateTablesAsync(CreateFlags.None, typeof(BookChapter)).ConfigureAwait(false);
                    await AddRangeAsync(_sampleChapters.CreateSampleChaptersData());
                }
                initialized = true;
            }
        }

        public async Task AddAsync(BookChapter bookChapter)
        {
            //bookChapter.Id = Guid.NewGuid();
            //_chapters[bookChapter.Id] = bookChapter;
            await DatabaseConnection.InsertAsync(bookChapter);
        }

        public async Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            //foreach (var chapter in chapters)
            //{
            //chapter.Id = Guid.NewGuid();
            //    //_chapters[chapter.Id] = chapter;
            //if (!DatabaseConnection.TableMappings.Any(m => m.MappedType.Name == nameof(BookChapter)))
            //{
                //foreach (var chapter in chapters)
                //{
                //    chapter.Id = Guid.NewGuid();
                //}
                await DatabaseConnection.InsertAllAsync(chapters);
            //}
            //}
            //return Task.CompletedTask;
        }

        public async Task<BookChapter> FindAsync(Guid id)
        {
            //_chapters.TryGetValue(id, out BookChapter bookChapter);
            //return Task.FromResult<BookChapter>(bookChapter);
            return await DatabaseConnection.Table<BookChapter>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookChapter>> GetAllAsync()
        {
            //Task.FromResult<IEnumerable<BookChapter>>(_chapters.Values);
            return await DatabaseConnection.Table<BookChapter>().ToListAsync();
        } 

        public async Task<BookChapter> RemoveAsync(Guid id)
        {
            var chapter = await DatabaseConnection.Table<BookChapter>().Where(c => c.Id == id).FirstOrDefaultAsync();
            //_chapters.TryRemove(id, out BookChapter remove);
            //return Task.FromResult<BookChapter>(remove);
            if (chapter != null)
            {
                await DatabaseConnection.DeleteAsync(chapter);
            }
            return chapter;
        }

        public async Task UpdateAsync(BookChapter bookChapter)
        {
            //_chapters[bookChapter.Id] = bookChapter;
            //return Task.CompletedTask;
            await DatabaseConnection.UpdateAsync(bookChapter);
        }
    }
    public static class TaskExtensions
    {
        public static async void SafeFireAndForget(this Task task, bool returnToCallingContext, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}

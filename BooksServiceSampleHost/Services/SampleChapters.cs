using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksServiceSampleHost.Models;

namespace BooksServiceSampleHost.Services
{
    public class SampleChapters
    {
        private readonly IBookChaptersService _bookChaptersService;
        public SampleChapters(IBookChaptersService bookChaptersService)
        {
            _bookChaptersService = bookChaptersService;
        }
        private string abc = "abcdefghijklmnopqrstuvwsyz";
        private string[] sampleTitles()
        {
            string[] strs = new string[100];
            Random rm = new Random();
            for (int i = 0; i < 100; i++)
            {
                var stringBuilder = new StringBuilder();
                for (int j = 0; j < 5; j++)
                {
                    stringBuilder.Append(abc[rm.Next(0, 26)].ToString());
                }
                strs[i] = stringBuilder.ToString();
            }
            return strs;
        } 
        private int[] chapterNumbers()
        {
            int[] nums = new int[100];
            Random rm = new Random();
            for (int i = 0; i < 100; i++)
            {
                nums[i] = rm.Next(0,1000);
            }
            return nums;
        }
        private int[] numberPages()
        {
            int[] nums_page = new int[100];
            Random rm = new Random();
            for (int i = 0; i < 100; i++)
            {
                nums_page[i] = rm.Next(0, 1000);
            }
            return nums_page;
        }
        public async Task CreateSampleChaptersAsync()
        {
            var chapters = new List<BookChapter>();
            for (int i = 0; i < 100; i++)
            {
                chapters.Add(new BookChapter
                {
                    //Id = Guid.NewGuid(),
                    Title = sampleTitles()[i],
                    Number = chapterNumbers()[i],
                    PublisherNumber = numberPages()[i],
                });
            }
            await _bookChaptersService.AddRangeAsync(chapters);
        }
    }
}

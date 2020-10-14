using System;
using System.Collections.Generic;

namespace BooksServiceSampleHost.Models
{
    public class BookChapter
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int PublisherNumber { get; set; }

    }
}

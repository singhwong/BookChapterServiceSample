using System;
using System.Collections.Generic;
using SQLite;

namespace BooksServiceSampleHost.Models
{
    public class BookChapter
    {
        [PrimaryKey,AutoIncrement]
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int PublisherNumber { get; set; }

    }
}

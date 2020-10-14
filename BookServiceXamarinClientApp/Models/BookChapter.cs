using System;
using BookServiceXamarinClientApp.Frameworks;

namespace BookServiceXamarinClientApp.Models
{
    public class BookChapter
    {
      public Guid Id { get; set; }
      public int Number { get; set; }
      public string Title { get; set; }
      public int PublisherNumber { get; set; }
    }
}

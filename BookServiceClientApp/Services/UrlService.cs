using System;
namespace BookServiceClientApp.Services
{
    public class UrlService
    {
        public string BaseAddress => "https://localhost:5001";
        public string BookApi => "api/BookChapters/";
    }
}

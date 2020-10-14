using System;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.Services
{
    public class UrlService
    {
        public string BaseAddress => Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        public string BookApi => "api/BookChapters/";
    }
}

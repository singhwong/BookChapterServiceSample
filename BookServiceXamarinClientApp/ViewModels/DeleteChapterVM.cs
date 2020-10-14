using System;
using System.Net.Http;
using System.Windows.Input;
using BookServiceXamarinClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.ViewModels
{
    public class DeleteChapterVM
    {
        public DeleteChapterVM()
        {
            var request = MainPageVM.ApplicationService.GetService<RequestService>();
            DeleteChapterCommand = new Command(async()=>
            {
                try
                {
                    if (Guid.TryParse(EnteredId, out var id))
                        await request.RemoveChapterAsync(id);
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("404"))
                {
                    await Application.Current.MainPage.DisplayAlert("alert","Not found! (404)","OK");
                }
            });
        }
        public string EnteredId { get; set; }
        public ICommand DeleteChapterCommand { get; set; }
    }
}

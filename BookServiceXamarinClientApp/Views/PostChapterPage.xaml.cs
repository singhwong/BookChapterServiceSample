using System;
using System.Collections.Generic;
using BookServiceXamarinClientApp.ViewModels;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.Views
{
    public partial class PostChapterPage : ContentPage
    {
        public PostChapterPage()
        {
            InitializeComponent();
            this.BindingContext = PostChapterVm;
        }
        public PostChapterVM PostChapterVm { get; } = new PostChapterVM();
    }
}

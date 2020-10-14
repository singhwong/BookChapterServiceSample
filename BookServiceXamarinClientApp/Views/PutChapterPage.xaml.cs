using System;
using System.Collections.Generic;
using BookServiceXamarinClientApp.ViewModels;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.Views
{
    public partial class PutChapterPage : ContentPage
    {
        public PutChapterPage()
        {
            InitializeComponent();
            this.BindingContext = PutVm;
        }
        public PutChapterVM PutVm { get; } = new PutChapterVM();
    }
}

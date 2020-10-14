using System;
using System.Collections.Generic;
using BookServiceXamarinClientApp.ViewModels;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.Views
{
    public partial class SingleChapterPage : ContentPage
    {
        public SingleChapterPage()
        {
            InitializeComponent();
            this.BindingContext = SingleVM;
        }
        public SingleChapterVM SingleVM { get; } = new SingleChapterVM();
    }
}

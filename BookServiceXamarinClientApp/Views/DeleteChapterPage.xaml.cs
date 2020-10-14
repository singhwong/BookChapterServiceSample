using System;
using System.Collections.Generic;
using BookServiceXamarinClientApp.ViewModels;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.Views
{
    public partial class DeleteChapterPage : ContentPage
    {
        public DeleteChapterPage()
        {
            InitializeComponent();
            this.BindingContext = DeleteVm;
        }
        public DeleteChapterVM DeleteVm { get; } = new DeleteChapterVM();
    }
}

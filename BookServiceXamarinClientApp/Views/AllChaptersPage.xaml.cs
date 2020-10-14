using System;
using System.Collections.Generic;
using BookServiceXamarinClientApp.ViewModels;
using Xamarin.Forms;

namespace BookServiceXamarinClientApp.Views
{
    public partial class AllChaptersPage : ContentPage
    {
        public AllChaptersPage()
        {
            InitializeComponent();
            this.BindingContext = ChaptersVm;
        }
        public AllChaptersVM ChaptersVm { get; } = new AllChaptersVM();
    }
}

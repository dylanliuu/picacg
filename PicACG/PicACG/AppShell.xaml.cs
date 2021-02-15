using System;
using System.Collections.Generic;
using PicACG.ViewModels;
using PicACG.Views;
using Xamarin.Forms;

namespace PicACG
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

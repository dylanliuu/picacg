using System.ComponentModel;
using Xamarin.Forms;
using PicACG.ViewModels;

namespace PicACG.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
using Portal.MAUI.Models;
using Portal.MAUI.PageModels;

namespace Portal.MAUI.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}
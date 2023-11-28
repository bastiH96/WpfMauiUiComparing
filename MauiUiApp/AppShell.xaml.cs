using CommunityToolkit.Maui.Views;
using MauiUiApp.HelperClasses;
using MauiUiApp.View;
using MauiUiApp.View.PopUp;
using MauiUiApp.ViewModel;
using MauiUiApp.ViewModel.PopUp;
using WpfMauiLibrary.Services;

namespace MauiUiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e) 
        {
            OpenCategoryNamePopUp();
        }

        private void OpenCategoryNamePopUp() 
        {
            var popup = new AddNewCategoryPopUpView(new AddNewCategoryPopUpViewModel());
            Shell.Current.CurrentPage.ShowPopup(popup);
            
        }

        
    }
}

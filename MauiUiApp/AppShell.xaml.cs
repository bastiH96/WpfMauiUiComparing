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
        private IReadOnlyList<Page> searchResultPage;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SearchResultView), typeof(SearchResultView));
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

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e) {
            var currentPage = Shell.Current.CurrentPage;
            if(e.OldTextValue == null || e.OldTextValue == string.Empty) {
                
                // searchResultPage = Shell.Current.CurrentPage.Navigation.NavigationStack;
            } 
            else if(e.NewTextValue != string.Empty)
            {
                if(currentPage is not SearchResultView searchResultView)
                {
                    await Navigation.PushAsync(new SearchResultView());
                    
                    
                }
            }
            else if(e.NewTextValue == null || e.NewTextValue == string.Empty)
            {
                await Navigation.PopAsync();
            }
        }
    }
}

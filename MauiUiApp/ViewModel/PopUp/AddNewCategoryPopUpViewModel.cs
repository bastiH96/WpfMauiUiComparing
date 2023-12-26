using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfMauiLibrary.HelperClasses;
using MauiUiApp.View;
using WpfMauiLibrary.Models;
using WpfMauiLibrary.Services;

namespace MauiUiApp.ViewModel.PopUp
{
    public partial class AddNewCategoryPopUpViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _categoryName;
        private CategoryDataAccess _db = new(Constants.DbFullPathMaui);

        [RelayCommand]
        public void CreateNewCategory() {
            if(CategoryName == null) {
                Toast.Make("You haven't entered a valid category name");
            }
            else {
                var category = new CategoryModel(CategoryName);
                _db.InsertOne(category);
                category.Id = _db.GetLastImplementedId();

                var view = new NewCategoryView(new NewCategoryViewModel(category));
                view.Title = category.Name;

                var shellcontent = new ShellContent() {
                    Title = category.Name,
                    Content = view,
                };
                
                AppShell.Current.Items.Add(shellcontent);
            } 
        }
    }
}

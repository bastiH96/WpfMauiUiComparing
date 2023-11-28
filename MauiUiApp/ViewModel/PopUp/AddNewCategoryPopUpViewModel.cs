using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiUiApp.HelperClasses;
using MauiUiApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMauiLibrary.Models;
using WpfMauiLibrary.Services;

namespace MauiUiApp.ViewModel.PopUp
{
    public partial class AddNewCategoryPopUpViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _categoryName;
        private CategoryDataAccess _db = new(Constants.DbFullPath);

        [RelayCommand]
        public void CreateNewCategory() {
            if(CategoryName == null) {
                Toast.Make("You haven't entered a valid category name");
            }
            else {
                var category = new CategoryModel(CategoryName, null, null);
                _db.InsertOne(category);

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

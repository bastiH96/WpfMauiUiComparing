using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiUiApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMauiLibrary.Models;

namespace MauiUiApp.ViewModel.PopUp
{
    public partial class AddNewCategoryPopUpViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _categoryName;

        [RelayCommand]
        public void CreateNewCategory() {
            if(CategoryName == null) {
                Toast.Make("You haven't entered a valid category name");
            }
            else {
                var category = new CategoryModel(CategoryName, null, null);
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

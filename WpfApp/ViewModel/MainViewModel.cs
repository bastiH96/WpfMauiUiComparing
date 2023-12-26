using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfApp.View.Pages;
using WpfMauiLibrary.Models;


namespace WpfApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    // PopUp
    [ObservableProperty]
    private string _newCategoryName;

    // Sidebar
    [ObservableProperty]
    private ObservableCollection<Button> _categoryButtons = new();

    // Frame- / Page-Control
    [ObservableProperty]
    private object _frameContent;


    [RelayCommand]
    private void CreateNewCategory()
    {
        CategoryModel category = new CategoryModel(NewCategoryName);
        Button newCategoryButton = new Button()
        {
            Content = NewCategoryName,
            Command = OpenCategoryPageCommand,
            CommandParameter = category
        };
        CategoryButtons.Add(newCategoryButton);
    }

    [RelayCommand]
    private void OpenCategoryPage(object category)
    {
        if(category is CategoryModel categoryModel)
        {
            FrameContent = new CategoryPage(categoryModel);
        }
    }
}

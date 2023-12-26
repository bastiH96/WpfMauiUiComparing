using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfApp.View.Pages;
using WpfMauiLibrary.Models;
using WpfMauiLibrary.Services;
using WpfMauiLibrary.HelperClasses;


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

    // Window-Control
    private CategoryDataAccess _categoryDb = new CategoryDataAccess(Constants.DbFullPathWpf);

    public MainViewModel()
    {
        AddDataToCategoryButtonsIfExist();
    }

    [RelayCommand]
    private void CreateNewCategory()
    {
        if(NewCategoryName != null && NewCategoryName != string.Empty)
        {
            CategoryModel category = new CategoryModel(NewCategoryName);
            _categoryDb.InsertOne(category);
            category.Id = _categoryDb.GetLastImplementedId();
            AddCategoryButtonToList(category);
        }
    }

    [RelayCommand]
    private void OpenCategoryPage(object category)
    {
        if(category is CategoryModel categoryModel)
        {
            FrameContent = new CategoryPage(categoryModel);
        }
    }

    private void AddDataToCategoryButtonsIfExist()
    {
        var categorys = _categoryDb.GetAll();
        foreach (var category in categorys)
        {
            AddCategoryButtonToList(category);
        }
    }

    private void AddCategoryButtonToList(CategoryModel category)
    {
        CategoryButtons.Add(new Button()
        {
            Content = category.Name,
            Command = OpenCategoryPageCommand,
            CommandParameter = category
        });
    }
}

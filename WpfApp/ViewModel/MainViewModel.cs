using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfApp.View.Pages;
using WpfMauiLibrary.Models;
using WpfMauiLibrary.Services;
using WpfMauiLibrary.HelperClasses;
using System.Windows;
using System.Configuration;
using WpfApp.ViewModel.Pages;


namespace WpfApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    // Searchbar
    [ObservableProperty] private string _searchText;
    [ObservableProperty] private Visibility _searchPageVisible;
    private ObservableCollection<ToDoTaskModel> _currentToDoTasks;
    private ToDoTaskDataAccess _toDoTaskDb = new(Constants.DbFullPathWpf);
    
    [ObservableProperty] private Visibility _searchTextPlaceholderVisibility = Visibility.Hidden;

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
    private CategoryDataAccess _categoryDb = new(Constants.DbFullPathWpf);

    public MainViewModel()
    {
        AddDataToCategoryButtonsIfExist();
    }

    

    [RelayCommand]
    private void CreateNewCategory()
    {
        if(NewCategoryName != null && NewCategoryName != string.Empty)
        {
            _categoryDb.InsertOne(new CategoryModel(NewCategoryName));
            var categoryId = _categoryDb.GetAll().Last().Id;
            CategoryModel newCategory = new(categoryId, NewCategoryName);
            AddCategoryButtonToList(newCategory);
        }
    }

    [RelayCommand]
    private void OpenCategoryPage(object category)
    {
        if(category is CategoryModel categoryModel)
        {
            FrameContent = new CategoryPage(categoryModel) 
            {
                Title = categoryModel.Name
            };
        }
    }

    [RelayCommand]
    private void SearchPageVisulisation() {
        ObservableCollection<ToDoTaskModel> results;
        _currentToDoTasks = new(_toDoTaskDb.GetAll());

        if (SearchText != null && SearchText != string.Empty) 
        {
            results = new(_currentToDoTasks.Where(x => x.Content.Contains(SearchText)));
        } else 
        {
            results = _currentToDoTasks;
        }
        var searchPageViewModel = new SearchPageViewModel(results);
        FrameContent = new SearchPage(searchPageViewModel);
    }

    [RelayCommand]
    private void SearchTextChanged(object obj) {
        if(obj is TextBox searchBar) {
            ObservableCollection<ToDoTaskModel> results = new(_currentToDoTasks.Where(x => x.Content.Contains(searchBar.Text)));
            var searchPageViewModel = new SearchPageViewModel(results);
            FrameContent = new SearchPage(searchPageViewModel);
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

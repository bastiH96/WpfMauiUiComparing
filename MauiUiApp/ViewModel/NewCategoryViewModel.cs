using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiUiApp.HelperClasses;
using System.Collections.ObjectModel;
using WpfMauiLibrary.Models;
using WpfMauiLibrary.Services;

namespace MauiUiApp.ViewModel;
public partial class NewCategoryViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _content;
    [ObservableProperty]
    private ObservableCollection<ToDoTaskModel> _toDoTasksOpen;
    [ObservableProperty]
    private ObservableCollection<ToDoTaskModel> _toDoTasksCompleted;
    [ObservableProperty]
    private bool _isVisible = false;
    private CategoryModel _category { get; set; }
    private ToDoTaskDataAccess _db; 
        
         

    public NewCategoryViewModel(CategoryModel category)
    {
        _category = category;
        _db = new(Constants.DbFullPath);
        ToDoTasksOpen = new ObservableCollection<ToDoTaskModel>(_db.GetAllByCategoryIdAndOpen(_category.Id));
        ToDoTasksCompleted = new ObservableCollection<ToDoTaskModel>(_db.GetAllByCategoryIdAndCompleted(_category.Id));
    }

    [RelayCommand]
    private void CreateNewToDoTask() 
    {
        if (Content == null) return;
        var toDoTask = new ToDoTaskModel(Content, null, null, _category.Id);
        _db.InsertOne(toDoTask);
        ToDoTasksOpen.Add(toDoTask);
        Content = string.Empty;
    }

    [RelayCommand]
    private void ChangeStatus(object obj) {
        if(obj is ToDoTaskModel toDoTaskModel)
        {
            if (ToDoTasksOpen.Contains(toDoTaskModel))
            {
                toDoTaskModel.IsCompleted = true;
                ToDoTasksOpen.Remove(toDoTaskModel);
                ToDoTasksCompleted.Add(toDoTaskModel);
            } else
            {
                toDoTaskModel.IsCompleted = false;
                ToDoTasksCompleted.Remove(toDoTaskModel);
                ToDoTasksOpen.Add(toDoTaskModel);
            }
            _db.UpdateOne(toDoTaskModel);
        }
    }
}


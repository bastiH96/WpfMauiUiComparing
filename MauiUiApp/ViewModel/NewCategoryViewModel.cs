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
    [ObservableProperty]
    private ToDoTaskModel _pageTaskModel;
        
         

    public NewCategoryViewModel(CategoryModel category)
    {
        _category = category;
        _db = new(Constants.DbFullPath);
        ToDoTasksOpen = new ObservableCollection<ToDoTaskModel>(_db.GetAllByCategoryIdAndOpen(_category.Id));
        ToDoTasksCompleted = new ObservableCollection<ToDoTaskModel>(_db.GetAllByCategoryIdAndCompleted(_category.Id));
        PageTaskModel = new(_category.Id);
    }

    [RelayCommand]
    private void CreateNewToDoTask() 
    {
        if (Content == null) return;
        PageTaskModel.Content = Content;
        _db.InsertOne(PageTaskModel);
        ToDoTasksOpen.Add(PageTaskModel);
        Content = string.Empty;
        PageTaskModel = new(Content, null, null, _category.Id);
    }

    [RelayCommand]
    private void ChangeStatus(object obj) {
        if(obj is ToDoTaskModel toDoTask)
        {
            _db.UpdateOne(toDoTask);
            if (ToDoTasksOpen.Contains(toDoTask))
            {
                toDoTask.IsCompleted = true;
                ToDoTasksOpen.Remove(toDoTask);
                ToDoTasksCompleted.Add(toDoTask);
            } else
            {
                toDoTask.IsCompleted = false;
                ToDoTasksCompleted.Remove(toDoTask);
                ToDoTasksOpen.Add(toDoTask);
            }
        }
    }

    [RelayCommand]
    private void DeleteToDoTask(object obj)
    {
        if(obj is ToDoTaskModel toDoTask)
        {
            _db.DeleteOne(toDoTask.Id);
            if (ToDoTasksOpen.Contains(toDoTask)){
                ToDoTasksOpen.Remove(toDoTask);
            } else
            {
                ToDoTasksCompleted.Remove(toDoTask);
            }
        }
    }

    [RelayCommand]
    private void Prio1(object obj)
    {
        UpdatePriority(obj, 0);
    }

    [RelayCommand]
    private void Prio2(object obj)
    {
        UpdatePriority(obj, 1);
    }

    [RelayCommand]
    private void Prio3(object obj)
    {
        UpdatePriority(obj, 2);
    }

    private void UpdatePriority(object obj, int priority)
    {
        if( obj is ToDoTaskModel toDoTask)
        {
            if (toDoTask.Priority == priority) return;
            toDoTask.Priority = priority;
            toDoTask.PriorityColor = Constants.PriorityColors[priority];
            UpdateList(toDoTask);
            _db.UpdateOne(toDoTask);
        }
    }

    private void UpdateList(ToDoTaskModel toDoTask)
    {
        if (ToDoTasksOpen.Contains(toDoTask))
        {
            var itemIndex = ToDoTasksOpen.IndexOf(toDoTask);
            ToDoTasksOpen[itemIndex] = toDoTask;
        } else
        {
            var itemIndex = ToDoTasksCompleted.IndexOf(toDoTask);
            ToDoTasksCompleted[itemIndex] = toDoTask;
        }
    }
}


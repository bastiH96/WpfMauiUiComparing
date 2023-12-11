using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiUiApp.HelperClasses;
using MauiUiApp.View.PopUp;
using Microsoft.UI.Xaml.Data;
using System.Collections.ObjectModel;
using WpfMauiLibrary.Models;
using WpfMauiLibrary.Services;

namespace MauiUiApp.ViewModel;
public partial class NewCategoryViewModel : ObservableObject
{
    // Task creation
    [ObservableProperty]
    private string? _content;
    private DateTime? _dueDateDate;
    public DateTime? DueDateDate {
        get => _dueDateDate;
        set {
            if (_dueDateDate != value) { 
                _dueDateDate = value;
                if(value == null) {
                    TimeIsVisible = false;
                }
                else
                {
                    TimeIsVisible = true;
                }
            }
        }
    }
    [ObservableProperty]
    private TimeSpan? _dueDateTime;
    [ObservableProperty]
    private int? _priority;

    [ObservableProperty]
    private ObservableCollection<ToDoTaskModel> _toDoTasksOpen;
    [ObservableProperty]
    private ObservableCollection<ToDoTaskModel> _toDoTasksCompleted;
    [ObservableProperty]
    private bool _isVisible = false;
    [ObservableProperty]
    private bool _timeIsVisible = false;
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
        DateTime? taskDueDate = ParseDateAndTime();
        ToDoTaskModel toDoTask = new(Content, taskDueDate, Priority, _category.Id);
        _db.InsertOne(toDoTask);
        ToDoTasksOpen.Add(toDoTask);
        Content = null;
        DueDateTime = null;
        DueDateDate = null;
        Priority = null;
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

    [RelayCommand]
    private void OpenFlagPopUp() 
    {
        var popup = new ChooseFlagPopUpView(this);
        Shell.Current.CurrentPage.ShowPopup(popup);
    }

    [RelayCommand]
    private void SetFlagWhileCreatingTask(object obj) {
        if (obj is string prio) {
            Priority = Convert.ToInt32(prio);
        }
    }

    private void UpdatePriority(object obj, int priority)
    {
        if( obj is ToDoTaskModel toDoTask)
        {
            if (toDoTask.Priority == priority) {
                toDoTask.Priority = null;
                toDoTask.PriorityColor = "#000000";
            } else {
                toDoTask.Priority = priority;
                toDoTask.PriorityColor = WpfMauiLibrary.HelperClasses.Constants.PriorityColors[priority];
            }
            UpdatePriorityInListElement(toDoTask);
            _db.UpdateOne(toDoTask);
        }
    }

    private void UpdatePriorityInListElement(ToDoTaskModel toDoTask)
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

    private DateTime? ParseDateAndTime() {
        if (DueDateDate != null) 
        {
            DateTime finalDueDate = Convert.ToDateTime(DueDateDate).Date;
            if (DueDateTime != null) 
            {
                finalDueDate += DueDateTime.Value;
            }
            return finalDueDate;
        }
        return null;
    }
}
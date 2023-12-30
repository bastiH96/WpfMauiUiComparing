using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfMauiLibrary.Models;
using WpfMauiLibrary.Services;
using WpfMauiLibrary.HelperClasses;
using System.Collections.ObjectModel;

namespace WpfApp.ViewModel.Pages;

public partial class CategoryViewModel : ObservableObject
{
    [ObservableProperty]
    private string _pageTitle;
    
    // New Task Creation
    [ObservableProperty] private string _taskContent;
    [ObservableProperty] private DateTime? _taskDueDate;
    [ObservableProperty] private int? _taskPriority;

    // Page Visulization
    [ObservableProperty] private ObservableCollection<ToDoTaskModel> _openTasks = new();
    [ObservableProperty] private ObservableCollection<ToDoTaskModel> _completedTasks = new();

    // Intern Page Control
    private CategoryModel _categoryModel;
    private ToDoTaskDataAccess _taskDb = new(Constants.DbFullPathWpf);

    public CategoryViewModel(CategoryModel categoryModel)
    {
        _categoryModel = categoryModel;
        PageTitle = _categoryModel.Name;
        LoadTasksFromDb();
        
    }

    [RelayCommand]
    private void CreateNewTask()
    {
        if (ValidateTaskContent())
        {
            ToDoTaskModel newTask = new(TaskContent, TaskDueDate, TaskPriority, _categoryModel.Id);
            _taskDb.InsertOne(newTask);
            newTask.Id = _taskDb.GetLastImplementedId();
            OpenTasks.Add(newTask);
            ClearTaskCreationFields();
        }
    }

    [RelayCommand]
    private void TaskStatusChanged(object obj)
    {
        if(obj is ToDoTaskModel toDoTask)
        {
            if(toDoTask.IsCompleted == true)
            {
                toDoTask.IsCompleted = false;
                CompletedTasks.Remove(toDoTask);
                OpenTasks.Add(toDoTask);
            } 
            else
            {
                toDoTask.IsCompleted = true;
                OpenTasks.Remove(toDoTask);
                CompletedTasks.Add(toDoTask);
            }
            _taskDb.UpdateOne(toDoTask);
        }
    }

    [RelayCommand]
    private void DeleteTask()
    {
        Console.WriteLine("hello World");
        //if(obj is ToDoTaskModel toDoTask)
        //{
        //    if (OpenTasks.Contains(toDoTask))
        //    {
        //        OpenTasks.Remove(toDoTask);
        //    } else
        //    {
        //        CompletedTasks.Remove(toDoTask);
        //    }
        //    _taskDb.DeleteOne(toDoTask.Id);
        //}
    }


    private bool ValidateTaskContent()
    {
        if (TaskContent != null 
            && TaskContent != string.Empty
            && TaskContent.Length >= 5)
        {
            return true;
        }
        return false;
    }

    private void ClearTaskCreationFields()
    {
        TaskContent = string.Empty;
        TaskDueDate = null;
        TaskPriority = null;
    }

    private void LoadTasksFromDb()
    {
        var allTasks = _taskDb.GetAll();
        var openTasksDb = allTasks.Where(x => x.IsCompleted == false && x.CategoryId == _categoryModel.Id).ToList();
        var completedTasksDb = allTasks.Where(x => x.IsCompleted == true && x.CategoryId == _categoryModel.Id).ToList();
        OpenTasks = new ObservableCollection<ToDoTaskModel>(openTasksDb);
        CompletedTasks = new ObservableCollection<ToDoTaskModel>(completedTasksDb);
    }
}

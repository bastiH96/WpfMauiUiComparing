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
}

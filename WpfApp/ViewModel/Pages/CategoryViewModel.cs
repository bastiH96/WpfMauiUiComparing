using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfMauiLibrary.Models;
using WpfMauiLibrary.Services;
using WpfMauiLibrary.HelperClasses;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp.ViewModel.Pages;

public partial class CategoryViewModel : ObservableObject {
    [ObservableProperty]
    private string _pageTitle;

    // New Task Creation
    [ObservableProperty] private string _taskContent;
    [ObservableProperty] private DateTime? _taskDueDate;
    [ObservableProperty] private int? _taskPriority;

    // Page Visulization
    [ObservableProperty] private ObservableCollection<ToDoTaskModel> _openTasks = new();
    [ObservableProperty] private ObservableCollection<ToDoTaskModel> _completedTasks = new();
    [ObservableProperty] private ToDoTaskModel _selectedTask;

    // Sorting
    [ObservableProperty]
    private ObservableCollection<string> _sortingOptions = new() {
        "Alphabetical",
        "Importance",
        "Due Date"
    };
    private string _selectedSortingOption;
    public string SelectedSortingOption {
        get => _selectedSortingOption;
        set {
            if (_selectedSortingOption != value) {
                _selectedSortingOption = value;
                SortingOptionChanged();
            } else {
                return;
            }
        }
    }
    [ObservableProperty] private string _sortingDirection;
    [ObservableProperty] private string _reverseButtonText;
    [ObservableProperty] private Visibility _reverseButtonVisibility = Visibility.Hidden;


    // Intern Page Control
    private CategoryModel _categoryModel;
    private ToDoTaskDataAccess _taskDb = new(Constants.DbFullPathWpf);

    public CategoryViewModel(CategoryModel categoryModel) {
        _categoryModel = categoryModel;
        PageTitle = _categoryModel.Name;
        LoadTasksFromDb();

    }

    [RelayCommand]
    private void CreateNewTask() {
        if (ValidateTaskContent()) {
            ToDoTaskModel newTask = new(TaskContent, TaskDueDate, TaskPriority, _categoryModel.Id);
            _taskDb.InsertOne(newTask);
            newTask.Id = _taskDb.GetLastImplementedId();
            OpenTasks.Add(newTask);
            ClearTaskCreationFields();
        }
    }

    [RelayCommand]
    private void TaskStatusChanged(object obj) {
        if (obj is ToDoTaskModel toDoTask) {
            if (toDoTask.IsCompleted == true) {
                toDoTask.IsCompleted = false;
                CompletedTasks.Remove(toDoTask);
                OpenTasks.Add(toDoTask);
            } else {
                toDoTask.IsCompleted = true;
                OpenTasks.Remove(toDoTask);
                CompletedTasks.Add(toDoTask);
            }
            _taskDb.UpdateOne(toDoTask);
        }
    }

    [RelayCommand]
    private void DeleteTask() {
        if (SelectedTask != null) {
            _taskDb.DeleteOne(SelectedTask.Id);
            if (OpenTasks.Contains(SelectedTask)) {
                OpenTasks.Remove(SelectedTask);
            } else {
                CompletedTasks.Remove(SelectedTask);
            }
        }
    }

    [RelayCommand]
    private void ReverseSorting() {
        if(SortingDirection == "Descending") {
            SortingOptionChangedReverse();
        } else {
            SortingOptionChanged();
        }
    }

    private void SortingOptionChanged() {
        ReverseButtonVisibility = Visibility.Visible;
        switch (SelectedSortingOption) {
            case "Alphabetical":
                OpenTasks = new ObservableCollection<ToDoTaskModel>(OpenTasks.OrderBy(x => x.Content));
                CompletedTasks = new ObservableCollection<ToDoTaskModel>(CompletedTasks.OrderBy(x => x.Content));
                ReverseButtonText = "Alphabetical ↓";
                break;
            case "Importance":
                break;
            case "Due Date":
                break;
        }
        SortingDirection = "Descending";
    }

    private void SortingOptionChangedReverse() {
        switch (SelectedSortingOption) {
            case "Alphabetical":
                OpenTasks = new ObservableCollection<ToDoTaskModel>(OpenTasks.Reverse());
                CompletedTasks = new ObservableCollection<ToDoTaskModel>(CompletedTasks.Reverse());
                ReverseButtonText = "Alphabetical ↑";
                break;
            case "Importance":
                break;
            case "Due Date":
                break;
        }
        SortingDirection = "Reverse";
    }

    private void ChangeSortingDirection() {
        if(SortingDirection == "Descending") {
            SortingDirection = "Reverse";
        } else {
            SortingDirection = "Descending";
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

    private void LoadTasksFromDb()
    {
        var allTasks = _taskDb.GetAll();
        var openTasksDb = allTasks.Where(x => x.IsCompleted == false && x.CategoryId == _categoryModel.Id).ToList();
        var completedTasksDb = allTasks.Where(x => x.IsCompleted == true && x.CategoryId == _categoryModel.Id).ToList();
        OpenTasks = new ObservableCollection<ToDoTaskModel>(openTasksDb);
        CompletedTasks = new ObservableCollection<ToDoTaskModel>(completedTasksDb);
    }
}

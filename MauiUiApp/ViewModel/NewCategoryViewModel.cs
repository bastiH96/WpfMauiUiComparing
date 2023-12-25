using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiUiApp.HelperClasses;
using MauiUiApp.View.PopUp;
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

    // Task Lists
    [ObservableProperty]
    private ObservableCollection<ToDoTaskModel> _toDoTasksOpen;
    [ObservableProperty]
    private ObservableCollection<ToDoTaskModel> _toDoTasksCompleted;

    // Sort function
    [ObservableProperty]
    private List<string> _orderOptions;
    [ObservableProperty]
    private string _selectedPickerItem;
    [ObservableProperty]
    private bool _isSortingActive = false;
    [ObservableProperty]
    private string _reverseButtonText;

    // Common page uses
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
        OrderOptions = new() {
            "Alphabetical",
            "Importance",
            "Due Date"
        };
        SelectedPickerItem = "------";
    }


    [RelayCommand]
    private void SortLists() 
    {
        var toDoTaskOpenList = ToDoTasksOpen.ToList();
        var toDoTaskCompletedList = ToDoTasksCompleted.ToList();
        switch (SelectedPickerItem) {
            case "Alphabetical":
                toDoTaskOpenList.Sort((x, y) => string.Compare(x.Content, y.Content));
                toDoTaskCompletedList.Sort((x, y) => string.Compare(x.Content, y.Content));
                ReverseButtonText = "Alphabetical ↑";
                IsSortingActive = true;
                break;
            case "Importance":
                SortByImportance(toDoTaskOpenList);
                SortByImportance(toDoTaskCompletedList);
                ReverseButtonText = "Importance ↓";
                IsSortingActive = true;
                break;
            case "Due Date":
                SortByDueDate(toDoTaskOpenList);
                SortByDueDate(toDoTaskCompletedList);
                ReverseButtonText = "Due Date ↓";
                IsSortingActive = true;
                break;
        }
        ToDoTasksOpen = new ObservableCollection<ToDoTaskModel>(toDoTaskOpenList);
        ToDoTasksCompleted = new ObservableCollection<ToDoTaskModel>(toDoTaskCompletedList);
    }

    [RelayCommand]
    private void ReverseSorting() {
        var toDoTaskOpenList = ToDoTasksOpen.ToList();
        var toDoTaskCompletedList = ToDoTasksCompleted.ToList();
        switch (ReverseButtonText) {
            case "Alphabetical ↑":
                toDoTaskOpenList.Sort((x, y) => string.Compare(y.Content, x.Content));
                toDoTaskCompletedList.Sort((x, y) => string.Compare(y.Content, x.Content));
                ReverseButtonText = "Alphabetical ↓";
                break;
            case "Alphabetical ↓":
                toDoTaskOpenList.Sort((x, y) => string.Compare(x.Content, y.Content));
                toDoTaskCompletedList.Sort((x, y) => string.Compare(x.Content, y.Content));
                ReverseButtonText = "Alphabetical ↑";
                break;
            case "Importance ↑":
                SortByImportance(toDoTaskOpenList);
                SortByImportance(toDoTaskCompletedList);
                ReverseButtonText = "Importance ↓";
                break;
            case "Importance ↓":
                SortByImportanceReverse(toDoTaskOpenList);
                SortByImportanceReverse(toDoTaskCompletedList);
                ReverseButtonText = "Importance ↑";
                break;
            case "Due Date ↑":
                SortByDueDate(toDoTaskOpenList);
                SortByDueDate(toDoTaskCompletedList);
                ReverseButtonText = "Due Date ↓";
                break;
            case "Due Date ↓":
                SortByDueDateReverse(toDoTaskOpenList);
                SortByDueDateReverse(toDoTaskCompletedList);
                ReverseButtonText = "Due Date ↑";
                break;
        }
        ToDoTasksOpen = new ObservableCollection<ToDoTaskModel>(toDoTaskOpenList);
        ToDoTasksCompleted = new ObservableCollection<ToDoTaskModel>(toDoTaskCompletedList);
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

    private void SortByImportance(List<ToDoTaskModel> unsortedList)
    {
        unsortedList.Sort((x, y) =>
        {
            if (x.Priority.HasValue && y.Priority.HasValue)
            {
                return x.Priority.Value.CompareTo(y.Priority.Value);
            }
            else if (x.Priority.HasValue)
            {
                return 1; // x ist größer als y, wenn x einen Wert hat und y nicht
            }
            else if (y.Priority.HasValue)
            {
                return -1; // x ist kleiner als y, wenn y einen Wert hat und x nicht
            }
            else
            {
                return 0; // x und y haben beide keinen Wert und sind gleich
            }
        });
    }

    private void SortByImportanceReverse(List<ToDoTaskModel> unsortedList)
    {
        unsortedList.Sort((x, y) =>
        {
            if (x.Priority.HasValue && y.Priority.HasValue)
            {
                return y.Priority.Value.CompareTo(x.Priority.Value);
            }
            else if (y.Priority.HasValue)
            {
                return 1; // y ist größer als x, wenn y einen Wert hat und x nicht
            }
            else if (x.Priority.HasValue)
            {
                return -1; // y ist kleiner als x, wenn x einen Wert hat und y nicht
            }
            else
            {
                return 0; // x und y haben beide keinen Wert und sind gleich
            }
        });
    }

    private void SortByDueDate(List<ToDoTaskModel> unsortedList)
    {
        unsortedList.Sort((x, y) =>
        {
            if(x.DueDate.HasValue && y.DueDate.HasValue)
            {
                return DateTime.Compare(x.DueDate.Value, y.DueDate.Value);
            }
            else if (x.DueDate.HasValue)
            {
                return 1;
            }
            else if (y.DueDate.HasValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });
    }

    private void SortByDueDateReverse(List<ToDoTaskModel> unsortedList)
    {
        unsortedList.Sort((x, y) =>
        {
            if (x.DueDate.HasValue && y.DueDate.HasValue)
            {
                return DateTime.Compare(y.DueDate.Value, x.DueDate.Value);
            }
            else if (y.DueDate.HasValue)
            {
                return 1;
            }
            else if (x.DueDate.HasValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });
    }
}
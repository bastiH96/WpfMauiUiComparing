using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMauiLibrary.Models;

namespace WpfApp.ViewModel.Pages; 
public partial class SearchPageViewModel : ObservableObject 
{
    [ObservableProperty] private ObservableCollection<ToDoTaskModel> _toDoTasks;

    public SearchPageViewModel(ObservableCollection<ToDoTaskModel> toDoTasks)
    {
        ToDoTasks = toDoTasks;
    }
}

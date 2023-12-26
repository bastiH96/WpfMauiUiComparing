using CommunityToolkit.Mvvm.ComponentModel;
using WpfMauiLibrary.HelperClasses;
using MauiUiApp.View;
using WpfMauiLibrary.Services;

namespace MauiUiApp.ViewModel;

public partial class AllTasksViewModel : ObservableObject
{
    private CategoryDataAccess _db = new(Constants.DbFullPathMaui);
    [ObservableProperty]
    private string _dbFullPath = Constants.DbFullPathMaui;

    public AllTasksViewModel()
    {
        CreateCategoryListFromDatabase();
    }

    private void CreateCategoryListFromDatabase() {
        var _db = new CategoryDataAccess(Constants.DbFullPathMaui);
        var categories = _db.GetAll();
        categories.ForEach(category => {
            var view = new NewCategoryView(new NewCategoryViewModel(category));
            view.Title = category.Name;

            var shellcontent = new ShellContent() {
                Title = category.Name,
                Content = view,
            };
            if (shellcontent != null) {
                AppShell.Current.Items.Add(shellcontent);
            }
        });
    }
}

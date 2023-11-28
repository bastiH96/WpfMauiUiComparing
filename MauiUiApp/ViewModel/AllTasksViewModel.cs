using CommunityToolkit.Mvvm.ComponentModel;
using MauiUiApp.HelperClasses;
using MauiUiApp.View;
using WpfMauiLibrary.Services;

namespace MauiUiApp.ViewModel;

public partial class AllTasksViewModel : ObservableObject
{
    private CategoryDataAccess _db = new(Constants.DbFullPath);

    public AllTasksViewModel()
    {
        CreateCategoryListFromDatabase();
    }

    private void CreateCategoryListFromDatabase() {
        var _db = new CategoryDataAccess(Constants.DbFullPath);
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

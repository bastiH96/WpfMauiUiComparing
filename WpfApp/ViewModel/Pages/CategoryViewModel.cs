using CommunityToolkit.Mvvm.ComponentModel;
using WpfMauiLibrary.Models;

namespace WpfApp.ViewModel.Pages;

public partial class CategoryViewModel : ObservableObject
{
    private CategoryModel _categoryModel;

    public CategoryViewModel(CategoryModel categoryModel)
    {
        _categoryModel = categoryModel;
    }
}

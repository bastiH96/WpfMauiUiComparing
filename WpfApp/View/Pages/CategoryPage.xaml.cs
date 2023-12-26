using System.Windows.Controls;
using WpfApp.ViewModel.Pages;
using WpfMauiLibrary.Models;

namespace WpfApp.View.Pages
{
    /// <summary>
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        public CategoryPage(CategoryModel category)
        {
            this.DataContext = new CategoryViewModel(category);
            InitializeComponent();
        }
    }
}

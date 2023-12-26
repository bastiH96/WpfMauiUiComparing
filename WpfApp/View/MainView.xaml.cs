using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.ViewModel;
using WpfMauiLibrary.HelperClasses;

namespace WpfApp.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            DatabaseHelper.CreateDatabaseWpf();
            DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void OpenPopUpButton_Clicked(object sender, RoutedEventArgs e)
        {
            newCategoryPopUp.IsOpen = true;
        }

        private void CreateNewCategoryButton_Clicked(object sender, RoutedEventArgs e)
        {
            newCategoryPopUp.IsOpen = false;
        }
    }
}

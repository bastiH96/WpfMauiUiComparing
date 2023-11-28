using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMauiLibrary.Models;

namespace MauiUiApp.ViewModel
{
    public partial class NewCategoryViewModel : ObservableObject
    {
        private CategoryModel _category { get; set; }
        
         

        public NewCategoryViewModel(CategoryModel category)
        {
            _category = category;
            
        }
    }
}

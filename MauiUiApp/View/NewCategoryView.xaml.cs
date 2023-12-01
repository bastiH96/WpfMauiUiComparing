using MauiUiApp.ViewModel;
using WinRT;

namespace MauiUiApp.View;

public partial class NewCategoryView : ContentPage
{
	public NewCategoryView(NewCategoryViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}
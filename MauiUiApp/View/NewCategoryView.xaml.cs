using MauiUiApp.ViewModel;

namespace MauiUiApp.View;

public partial class NewCategoryView : ContentPage
{
	public NewCategoryView(NewCategoryViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) {

    }
}
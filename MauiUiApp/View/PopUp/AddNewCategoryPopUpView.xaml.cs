using CommunityToolkit.Maui.Views;
using MauiUiApp.ViewModel.PopUp;

namespace MauiUiApp.View.PopUp;

public partial class AddNewCategoryPopUpView : Popup
{
	public AddNewCategoryPopUpView(AddNewCategoryPopUpViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
	
	private void Button_Clicked(object sender, EventArgs e) => Close();
}
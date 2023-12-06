using CommunityToolkit.Maui.Views;
using MauiUiApp.ViewModel;

namespace MauiUiApp.View.PopUp;

public partial class ChooseFlagPopUpView : Popup
{
	public ChooseFlagPopUpView(NewCategoryViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext=viewmodel;
	}

	private void Button_Clicked(object sender, EventArgs e) => Close();
}
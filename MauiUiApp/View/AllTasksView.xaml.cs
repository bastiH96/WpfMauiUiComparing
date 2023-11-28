using MauiUiApp.ViewModel;

namespace MauiUiApp.View;

public partial class AllTasksView : ContentPage
{
	public AllTasksView(AllTasksViewModel viewmodel)
	{
        InitializeComponent();
        BindingContext = viewmodel;
    }
}
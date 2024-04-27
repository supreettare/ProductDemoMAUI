namespace DemoAppOne.Views;

public partial class AddOrEditProductView : ContentPage
{
	private AddOrEditProductViewModel _viewModel;
    public AddOrEditProductView(AddOrEditProductViewModel addOrEditProductViewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = addOrEditProductViewModel;
	}

    //handling save,update and delete buttons visibility
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.SetButtonsVisibility();
    }
}
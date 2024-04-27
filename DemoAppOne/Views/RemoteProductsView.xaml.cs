namespace DemoAppOne.Views;

public partial class RemoteProductsView : ContentPage
{
    private RemoteProductsViewModel _viewModel;
	public RemoteProductsView(RemoteProductsViewModel remoteProductsViewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = remoteProductsViewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.FetchProducts();
        storeCollectionView.SelectedItem = null;
    }
}
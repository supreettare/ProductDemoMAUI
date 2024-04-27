namespace DemoAppOne.Views;

public partial class ProductListView : ContentPage
{
	private ProductListViewModel _viewModel;
    public ProductListView(ProductListViewModel productListViewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = productListViewModel;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.FetchProducts();
        productsCollectionView.SelectedItem = null;
    }
}
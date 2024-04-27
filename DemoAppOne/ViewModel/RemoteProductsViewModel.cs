namespace DemoAppOne.ViewModel
{
    public partial class RemoteProductsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool isNoProductLabelVisible;
        [ObservableProperty]
        public ObservableCollection<Product> storeProducts = new ObservableCollection<Product>();
        [ObservableProperty]
        private Product _selectedProduct;
        public RemoteProductsViewModel(IDemoAppOneDatabase _demoAppOneDatabase,
                                      INavigationService _navigationService)
        : base(_demoAppOneDatabase, _navigationService)
        {
        }

        [RelayCommand]
        private async Task StoreProductSelected(Product product)
        {
            Product newProduct = product;
            newProduct.ProductId = 0;
            await _navigationService.NavigateToAsync(nameof(AddOrEditProductView),
                    new Dictionary<string, object>
                    {
                        { "ProductModel" ,newProduct },
                        { "IsStoreProduct" ,true },
                    });
        }

        public async Task FetchProducts()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;
                await App.CheckInternetConnectivity();
                var products = await _demoAppOneDatabase.FetchRemoteProducts();

                if (products is not null)
                {
                    // Maps the retrieved products to Product objects, setting a default image if the ProductImage is null or empty.
                    StoreProducts = new ObservableCollection<Product>(products.Select(p => new Product
                    {
                        // Set a default image if ProductImage is null or empty
                        ProductImage = string.IsNullOrEmpty(p.ProductImage) ? "placeholder.png" : p.ProductImage,
                        Name = p.Name,
                        Description = p.Description,
                        Cost = p.Cost,
                        Quantity = p.Quantity,
                        ProductId = p.ProductId
                    }));
                }

                IsNoProductLabelVisible = StoreProducts.Count is 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert(StringResources.BrandName, StringResources.ErrorMessage, StringResources.OK);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

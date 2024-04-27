namespace DemoAppOne.ViewModel
{
    public partial class ProductListViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        public ObservableCollection<Product> products = new ObservableCollection<Product>();
        [ObservableProperty]
        private Product _selectedProduct;
        [ObservableProperty]
        private bool isNoProductLabelVisible;

        #endregion

        public ProductListViewModel(IDemoAppOneDatabase _demoAppOneDatabase, 
                                    INavigationService _navigationService)
        : base(_demoAppOneDatabase, _navigationService)
        {
        }

        // Adds a new product by navigating to the AddOrEditProductView page with the IsNewProduct flag set to true.
        [RelayCommand]
        private async Task AddProduct()
        {
            await _navigationService.NavigateToAsync(nameof(AddOrEditProductView),
                    new Dictionary<string, object>
                    {
                        { "IsNewProduct", true }
                    }); 
        }

        // Edits the selected product by navigating to the AddOrEditProductView page with the selected product's model passed as a parameter.
        [RelayCommand]
        public async Task EditProduct()
        {
            if (SelectedProduct == null)
            {
                return;
            }
            await _navigationService.NavigateToAsync(nameof(AddOrEditProductView),
                    new Dictionary<string, object>
                    {
                        { "ProductModel", SelectedProduct }
                    });
        }

        // Refreshes the list of products by fetching them again from the database and updating the UI.
        [RelayCommand]
        private async Task RefreshProducts()
        {
            await FetchProducts();
            IsBusy = false;
        }

        // Fetches products from the database and updates the Products collection with the retrieved data.
        public async Task FetchProducts()
        {
            try
            {
                // Retrieves products asynchronously from the database.
                var products = await _demoAppOneDatabase.GetProductsAsync();
                // Maps the retrieved products to Product objects, setting a default image if the ProductImage is null or empty.
                Products = new ObservableCollection<Product>(products.Select(p => new Product
                {
                    // Set a default image if ProductImage is null or empty
                    ProductImage = string.IsNullOrEmpty(p.ProductImage) ? "placeholder.png" : p.ProductImage,
                    Name = p.Name,
                    Description = p.Description,
                    Cost = p.Cost,
                    Quantity = p.Quantity,
                    ProductId = p.ProductId
                }));

                IsNoProductLabelVisible = Products.Count is 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert(StringResources.BrandName, StringResources.ErrorMessage, StringResources.OK);
            }
        }
    }
}

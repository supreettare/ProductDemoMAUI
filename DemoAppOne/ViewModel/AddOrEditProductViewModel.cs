namespace DemoAppOne.ViewModel
{
    [QueryProperty(nameof(IsNewProduct), "IsNewProduct")]
    [QueryProperty(nameof(IsStoreProduct), "IsStoreProduct")]
    [QueryProperty(nameof(ProductModel), nameof(ProductModel))]
    public partial class AddOrEditProductViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        private bool isDeleteButtonVisible;
        [ObservableProperty]
        private bool _isUpdateButtonVisible;
        [ObservableProperty]
        private bool _isSaveButtonVisible;
        [ObservableProperty]
        private bool isNewProduct;
        [ObservableProperty]
        private bool isStoreProduct;
        [ObservableProperty]
        private Product _productModel;
        [ObservableProperty]
        private string title;
        private readonly IImageService _imageService;
        #endregion

        public AddOrEditProductViewModel(IDemoAppOneDatabase _demoAppOneDatabase,
                                         IImageService imageService,
                                         INavigationService _navigationService)
        : base(_demoAppOneDatabase, _navigationService)
        {
            _imageService = imageService;
        }

        // Sets the visibility of buttons and initializes product data based on whether it's a new product or an existing one.
        public async Task SetButtonsVisibility()
        {
            if (IsStoreProduct)
            {
                Title = StringResources.AddProduct;
                IsSaveButtonVisible = true;
                IsDeleteButtonVisible = IsUpdateButtonVisible = false;
            }
            else
            {
                if (IsNewProduct)
                {
                    // Setting title and initializing product model for adding a new product.
                    Title = StringResources.AddProduct;
                    ProductModel = new Product
                    {
                        ProductImage = "placeholder.png"
                    };
                    IsSaveButtonVisible = true;
                    IsDeleteButtonVisible = IsUpdateButtonVisible = false;
                }
                else
                {
                    // Setting title and visibility for updating an existing product.
                    Title = StringResources.UpdateProduct;
                    IsSaveButtonVisible = false;
                    IsDeleteButtonVisible = IsUpdateButtonVisible = true;
                }
            }
        }

        // Allows the user to change the product image by either taking a photo with the camera or selecting from the gallery.
        [RelayCommand]
        public async Task ChangeProductImage()
        {
            try
            {
                // Display action sheet to select an option: Camera, Gallery, or Cancel.
                var action = await App.Current.MainPage.DisplayActionSheet("Select an option", null, "Cancel", "Camera", "Gallery");
                string photo;
                switch (action)
                {
                    case "Camera":
                        // Capture a photo using the camera and update the product image if a photo is captured.
                        photo = await _imageService.OpenCamera();
                        if (photo is not null)
                        {
                            ProductModel.ProductImage = photo;
                        }
                        break;
                    case "Gallery":
                        // Pick a photo from the gallery and update the product image if a photo is picked.
                        photo = await _imageService.PickPhotoFromGallery();
                        if (photo is not null)
                        {
                            ProductModel.ProductImage = photo;
                        }
                        break;
                    case "Cancel": break; // No action needed if canceled.
                    default: break; // No action needed for other cases.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert(StringResources.BrandName, StringResources.ErrorMessage, StringResources.OK);
            }
        }

        // Creates or updates the product based on whether it's a new product or an existing one.
        [RelayCommand]
        private async Task CreateOrUpdateProduct()
        {
            try
            {
                if (ProductModel != null)
                {
                    // Check if the product name is empty and display an alert if so.
                    if (string.IsNullOrWhiteSpace(ProductModel.Name))
                    {
                        await Shell.Current.DisplayAlert(StringResources.Alert, StringResources.NameRequired, StringResources.OK);
                        return;
                    }
                    // Save the product to the database and navigate back.
                    await _demoAppOneDatabase.SaveProductAsync(ProductModel);
                    //toast message
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                        await Toast.Make(StringResources.SavedSuccessfully,
                        ToastDuration.Long,
                                         16).Show(cancellationTokenSource.Token);
                        //navigate back to dashboard page
                        await _navigationService.NavigateToRootAsync();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert(StringResources.BrandName, StringResources.ErrorMessage, StringResources.OK);
            }
        }

        // Deletes the product after confirmation.
        [RelayCommand]
        private async Task DeleteProduct()
        {
            try
            {
                var response = await Shell.Current.DisplayAlert(StringResources.Alert, StringResources.DeleteConfirmation, StringResources.Yes, StringResources.No);
                if (response)
                {
                    // Delete the product from the database and navigate back.
                    await _demoAppOneDatabase.DeleteProductAsync(ProductModel);
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    await Toast.Make(StringResources.DeletedSuccessfully,
                                     ToastDuration.Long,
                                     16).Show(cancellationTokenSource.Token);
                    //navigate back to dashboard page
                    await _navigationService.NavigateBackAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert(StringResources.BrandName, StringResources.ErrorMessage, StringResources.OK);
            }
        }

        [RelayCommand]
        private async Task GetRemoteProducts()
        {
            await _navigationService.NavigateToAsync(nameof(RemoteProductsView));
        }
    }
}

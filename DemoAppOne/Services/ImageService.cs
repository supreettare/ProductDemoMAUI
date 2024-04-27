namespace DemoAppOne.Services
{
    class ImageService : IImageService
    {
        // Opens the device camera and returns the path of the captured photo.
        public async Task<string> OpenCamera()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Capture a photo using the default media picker.
                FileResult file = await MediaPicker.Default?.CapturePhotoAsync();
                // Load and return the path of the captured photo.
                return await LoadPhotoAsync(file);
            }
            // Return null if capturing photos is not supported.
            return null;
        }

        // Picks a photo from the device gallery and returns its path.
        public async Task<string> PickPhotoFromGallery()
        {
            if (MediaPicker.Default.IsCaptureSupported || Device.RuntimePlatform == Device.iOS)
            {
                // Pick a photo from the device gallery using the default media picker.
                FileResult file = await MediaPicker.Default?.PickPhotoAsync();
                // Load and return the path of the picked photo.
                return await LoadPhotoAsync(file);
            }
            // Return null if picking photos from the gallery is not supported.
            return null;
        }

        // Loads the photo asynchronously and saves it to the device's cache directory.
        private async Task<string> LoadPhotoAsync(FileResult photo)
        {
            if (photo is null)
            {
                // Return an empty string if the photo is null.
                return "";
            }
            // Create a new file path in the cache directory.
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            // Open a stream to the original photo and copy it to the new file asynchronously.
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            // Return the path of the new file.
            return newFile;
        }

    }
}

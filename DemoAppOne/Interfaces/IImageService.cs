namespace DemoAppOne.Interfaces
{
    public interface IImageService
    {
        Task<string> OpenCamera();
        Task<string> PickPhotoFromGallery();
    }
}

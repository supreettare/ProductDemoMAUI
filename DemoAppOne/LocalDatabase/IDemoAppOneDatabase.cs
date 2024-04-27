namespace DemoAppOne.LocalDatabase
{
    public interface IDemoAppOneDatabase
    {
        Task<int> SaveProductAsync(Product product);
        Task<Product> GetProductAsync(int id);
        Task<List<Product>> GetProductsAsync();
        Task<int> DeleteProductAsync(Product product);
        Task<List<Product>> FetchRemoteProducts();
    }
}

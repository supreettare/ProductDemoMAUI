using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;

namespace DemoAppOne.LocalDatabase
{
    public class DemoAppOneDatabase : IDemoAppOneDatabase
    {
        public async Task<List<Product>> FetchRemoteProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMilliseconds(5000);
                    string url = "https://fakestoreapi.com/products";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Deserialize the response body to List<Product>
                        products = JsonConvert.DeserializeObject<List<StoreProduct>>(responseBody)
                                    .Select(storeProduct => new Product
                                    {
                                        Name = storeProduct.title,
                                        Description = storeProduct.description,
                                        Cost = storeProduct.price,
                                        ProductImage = storeProduct.image,
                                        Quantity = 10,
                                        ProductId = 0,
                                    })
                                    .ToList();
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch products. Status code: {response.StatusCode}");
                        // Optionally, you can throw an exception or handle the error accordingly.
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
                // Optionally, you can throw an exception or handle the error accordingly.
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Request timed out: {ex.Message}");
                // Optionally, you can throw an exception or handle the error accordingly.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Optionally, you can throw an exception or handle the error accordingly.
            }
            return products;
        }

            // Method to retrieve all products from the database
        public async Task<List<Product>> GetProductsAsync()
        {
            // Ensure database is initialized
            await Helper.Constants.Init();
            // Retrieve all products from the Product table
            var result = await Helper.Constants.Database.Table<Product>().ToListAsync();
            // Check if result is null or empty
            if (result is null || result.Count == 0)
            {
                // Return an empty list if no products found
                return new List<Product>();
            }
            return result;
        }

        // Method to retrieve a product by its ID
        public async Task<Product> GetProductAsync(int id)
        {
            // Ensure database is initialized
            await Helper.Constants.Init();
            // Retrieve the product with the specified ID
            return await Helper.Constants.Database.Table<Product>().Where(i => i.ProductId == id).FirstOrDefaultAsync();
        }

        // Method to save a product to the database
        public async Task<int> SaveProductAsync(Product product)
        {
            // Ensure database is initialized
            await Helper.Constants.Init();
            // Check if the product ID is set
            if (product.ProductId != 0)
                // Update the product if it already exists
                return await Helper.Constants.Database.UpdateAsync(product);
            else
                // Insert the product if it's a new one
                return await Helper.Constants.Database.InsertAsync(product);
        }

        // Method to delete a product from the database
        public async Task<int> DeleteProductAsync(Product product)
        {
            // Ensure database is initialized
            await Helper.Constants.Init();
            // Delete the specified product from the database
            return await Helper.Constants.Database.DeleteAsync(product);
        }
    }
}

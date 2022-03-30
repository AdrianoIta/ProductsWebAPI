using ProductsWebAPI.Models;

namespace ProductsWebAPI.Business
{
    public interface IProductsBusiness
    {
        List<ProductsModel> GetProducts();

        ProductsModel GetProductById(string id);

        void AddProducts(ProductsModel product);

        long UpdateProduct(ProductsModel product);

        void DeleteProductById(string id);
    }
}

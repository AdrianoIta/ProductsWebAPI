using MongoDB.Driver;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Validators
{
    public interface IProductsValidator
    {
        void ValidateRequiredFields(ProductsModel products);
        void ProductAreDuplicated(ProductsModel product, List<ProductsModel> products);
        void ProductWasDeleted(DeleteResult deleted);
        void ProductsExists(List<ProductsModel> products);
    }
}

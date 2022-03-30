using MongoDB.Driver;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Validators
{
    public static class ProductsValidator 
    {
        public static void ValidateRequiredFields(ProductsModel products)
        {
            if (string.IsNullOrEmpty(products.ProductName))
                throw new ArgumentNullException("The category should not be empty.");

            if (string.IsNullOrEmpty(products.Category))
                throw new ArgumentNullException("The category should not be empty.");

            if (products.UnitPrice <= 0)
                throw new ArgumentNullException("The unit price must be greater than 0.");
        }

        public static void ProductAreDuplicated(ProductsModel productToAdd, List<ProductsModel> products)
        {
            var isDuplicated = products.Any(x => x.ProductName == productToAdd.ProductName && x.Category == productToAdd.Category);

            if (isDuplicated)
                throw new Exception("The product you're trying to add already exists.");
        }

        public static void ProductWasDeleted(DeleteResult deleted)
        {
            if (deleted.DeletedCount <= 0)
                throw new ArgumentNullException("The product was not deleted due to not exist.");
        }

        public static void ProductsExists(List<ProductsModel> products) 
        {
            if (!products.Any())
                throw new ArgumentNullException("No products found.");
        }
    }
}

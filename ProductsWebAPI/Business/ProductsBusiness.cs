using MongoDB.Driver;
using ProductsWebAPI.Helpers;
using ProductsWebAPI.Models;
using ProductsWebAPI.Validators;

namespace ProductsWebAPI.Business
{
    public class ProductsBusiness : IProductsBusiness
    {
        private readonly IProductsValidator ProductsValidator;

        public ProductsBusiness() { }

        protected ProductsBusiness(IProductsValidator productsValidator)
        {
            ProductsValidator = productsValidator;
        }

        public void AddProducts(ProductsModel product)
        {
            var existentProducts = MongoDbHelpers.GetProductsCollection().AsQueryable();

            ProductsValidator.ValidateRequiredFields(product);
            ProductsValidator.ProductAreDuplicated(product, existentProducts.ToList());

            MongoDbHelpers.GetProductsCollection().InsertOne(product);
        }

        public void DeleteProductById(string id)
        {
            var collection = MongoDbHelpers.GetProductsCollection();

            var filter = Builders<ProductsModel>.Filter.Eq("Id", id);

            var result = collection.DeleteOne(filter);

            ProductsValidator.ProductWasDeleted(result);
        }

        public ProductsModel GetProductById(string id)
        {
            var collection = MongoDbHelpers.GetProductsCollection();

            var filter = Builders<ProductsModel>.Filter.Eq("Id", id);

            var product = collection.Find(filter).First();

            return product;
        }

        public List<ProductsModel> GetProducts()
        {
            var collection = MongoDbHelpers.GetProductsCollection().AsQueryable().ToList();

            ProductsValidator.ProductsExists(collection);

            return collection;
        }

        public void UpdateProduct(ProductsModel product)
        {
            var collection = MongoDbHelpers.GetProductsCollection();

            var filter = Builders<ProductsModel>.Filter.Eq("Id", product.Id);
            var update = Builders<ProductsModel>.Update;
            var updates = new List<UpdateDefinition<ProductsModel>>();

            if (!string.IsNullOrEmpty(product.ProductName))
                updates.Add(update.Set("ProductName", product.ProductName));
            if (product.UnitPrice > 0)
                updates.Add(update.Set("UnitPrice", product.UnitPrice));
            if (!string.IsNullOrEmpty(product.Category))
                updates.Add(update.Set("Category", product.Category));

            collection.UpdateOne(filter, update.Combine(updates));
        }
    }
}

using MongoDB.Driver;
using ProductsWebAPI.Helpers;
using ProductsWebAPI.Models;
using ProductsWebAPI.Validators;

namespace ProductsWebAPI.Business
{
    public class ProductsBusiness : IProductsBusiness
    {
        public ProductsBusiness() { }

        public void AddProducts(ProductsModel product)
        {
            var existentProducts = MongoDbHelpers.GetProductsCollection().AsQueryable();

            ProductsValidator.ValidateRequiredFields(product);
            ProductsValidator.ProductAreDuplicated(product, existentProducts.ToList());

            product.ProductId = Guid.NewGuid().ToString();
            MongoDbHelpers.GetProductsCollection().InsertOne(product);
        }

        public void DeleteProductById(string id)
        {
            var collection = MongoDbHelpers.GetProductsCollection();

            var filter = Builders<ProductsModel>.Filter.Eq("ProductId", id);

            var result = collection.DeleteOne(filter);

            ProductsValidator.ProductWasDeleted(result);
        }

        public ProductsModel GetProductById(string id)
        {
            var collection = MongoDbHelpers.GetProductsCollection();

            var filter = Builders<ProductsModel>.Filter.Eq("ProductId", id);

            var product = collection.Find(filter).First();

            return product;
        }

        public List<ProductsModel> GetProducts()
        {
            var collection = MongoDbHelpers.GetProductsCollection().AsQueryable().ToList();

            ProductsValidator.ProductsExists(collection);

            return collection;
        }

        public long UpdateProduct(ProductsModel product)
        {
            var collection = MongoDbHelpers.GetProductsCollection();

            var filter = Builders<ProductsModel>.Filter.Eq("ProductId", product.ProductId);
            var update = Builders<ProductsModel>.Update;
            var updates = new List<UpdateDefinition<ProductsModel>>();

            if (!string.IsNullOrEmpty(product.ProductName))
                updates.Add(update.Set("ProductName", product.ProductName));
            if (product.UnitPrice > 0)
                updates.Add(update.Set("UnitPrice", product.UnitPrice));
            if (!string.IsNullOrEmpty(product.Category))
                updates.Add(update.Set("Category", product.Category));

            var updatedRecord = collection.UpdateOne(filter, update.Combine(updates));

            return updatedRecord.ModifiedCount;
        }

       
    }
}

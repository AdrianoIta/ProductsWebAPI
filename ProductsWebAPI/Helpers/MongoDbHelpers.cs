using MongoDB.Driver;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Helpers
{
    public static class MongoDbHelpers
    {
        private static readonly string DatabaseName = "complevotestdb";
        private static readonly string ConnectionString = "mongodb+srv://complevoapitest:6OcvU9OxIo0eirn6@cluster0.vxytb.mongodb.net/myFirstDatabase";
        private static IMongoDatabase GetMongoDatabase()
        {
            var mongoUrl = MongoUrl.Create(ConnectionString);

            return new MongoClient(mongoUrl).GetDatabase(DatabaseName);
        }

        public static IMongoCollection<ProductsModel> GetProductsCollection()
        {
            return GetMongoDatabase().GetCollection<ProductsModel>("Products");
        }
    }
}

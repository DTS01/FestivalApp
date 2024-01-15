using MongoDB.Driver;

namespace FestivalApp.Shared.Storage
{
    public class DataStorage
    {
        private readonly IMongoClient dbClient; // Client for interacting with MongoDB server
        private readonly IMongoDatabase database; // Database object for performing operations on the database

        // Constructor establishes a connection to the database using the provided connection string
        public DataStorage()
        {
            // Connection string
            string connectionString = "mongodb+srv://eaa23tsd:HofteUdhus30@cluster0.cha8yhm.mongodb.net/";
            dbClient = new MongoClient(connectionString);
            database = dbClient.GetDatabase("MusikDB");
        }

        // Method to retrieve the MongoDB database object
        public IMongoDatabase GetDatabase()
        {
            return database;
        }
    }
}

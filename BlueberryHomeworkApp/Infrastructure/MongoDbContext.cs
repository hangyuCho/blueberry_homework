using MongoDB.Driver;

namespace BlueberryHomeworkApp.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName = "blueberry_db")
        {
            // MongoDB 연결
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // 데이터베이스에서 컬렉션 가져오기
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
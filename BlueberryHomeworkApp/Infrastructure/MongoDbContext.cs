using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using BlueberryHomeworkApp.Domain.Entities;

namespace BlueberryHomeworkApp.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            // MongoDB 연결
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("blueberry_db");

            // MongoDB 매핑 설정
            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                BsonClassMap.RegisterClassMap<User>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(u => u.Id); // Id를 _id로 매핑
                    cm.MapMember(u => u.Email).SetDefaultValue((User u) => u.Id.ToString() + "@temp.com");
                    cm.SetIgnoreExtraElements(true); // 추가 필드 무시
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Company)))
            {
                BsonClassMap.RegisterClassMap<Company>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id);
                    cm.MapMember(c => c.Name).SetIsRequired(true);
                    cm.MapMember(c => c.CreatedAt).SetIsRequired(true);
                    cm.MapMember(c => c.UpdatedAt).SetIsRequired(false);
                    cm.MapMember(c => c.Address).SetDefaultValue("N/A");
                    cm.MapMember(c => c.IsActive).SetDefaultValue(true); // IsActive 기본값을 true로 설정
                    cm.SetIgnoreExtraElements(true); // 추가 필드 무시
                });
            }
        }

        // 데이터베이스에서 컬렉션 가져오기
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public IMongoDatabase Database => _database;
    }
}
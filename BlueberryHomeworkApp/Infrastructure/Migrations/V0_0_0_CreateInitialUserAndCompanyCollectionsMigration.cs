using Mongo.Migration.Migrations;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlueberryHomeworkApp.Infrastructure.Migrations;

public class V0_0_0_CreateInitialUserAndCompanyCollectionsMigration : MigrationBase
{
    public V0_0_0_CreateInitialUserAndCompanyCollectionsMigration(IMongoDatabase database) : base(database)
    {
    }

    public override string Version => "0.0.0";

    public override async Task Up()
    {
        Console.WriteLine("V0_0_0 마이그레이션 시작");
        
        // User 컬렉션 생성
        Console.WriteLine("User 컬렉션 생성 시작");
        var userCollection = Database.GetCollection<BsonDocument>("user");
        
        // User 컬렉션 인덱스 생성
        var userIndexes = new List<CreateIndexModel<BsonDocument>>
        {
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Id"),
                new CreateIndexOptions { Unique = true }
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Name"),
                new CreateIndexOptions { Unique = true }
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("CreatedAt")
            )
        };
        await userCollection.Indexes.CreateManyAsync(userIndexes);
        Console.WriteLine("User 컬렉션 생성 완료");

        // Company 컬렉션 생성
        Console.WriteLine("Company 컬렉션 생성 시작");
        var companyCollection = Database.GetCollection<BsonDocument>("company");
        
        // Company 컬렉션 인덱스 생성
        var companyIndexes = new List<CreateIndexModel<BsonDocument>>
        {
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Id"),
                new CreateIndexOptions { Unique = true }
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Name"),
                new CreateIndexOptions { Unique = true }
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("UserId")
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("CreatedAt")
            )
        };
        await companyCollection.Indexes.CreateManyAsync(companyIndexes);
        Console.WriteLine("Company 컬렉션 생성 완료");
        
        Console.WriteLine("V0_0_0 마이그레이션 완료");
    }

    public override async Task Down()
    {
        await Database.DropCollectionAsync("user");
        await Database.DropCollectionAsync("company");
    }
}
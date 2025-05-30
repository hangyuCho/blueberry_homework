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
        Console.WriteLine("마이그레이션 시작: V0_0_0_CreateInitialUserAndCompanyCollectionsMigration");
        
        // Users 컬렉션 생성
        Console.WriteLine("Users 컬렉션 생성 시작");
        var userCollection = Database.GetCollection<BsonDocument>("users");
        
        // Users 컬렉션 인덱스 생성
        var userIndexes = new List<CreateIndexModel<BsonDocument>>
        {
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Id"),
                new CreateIndexOptions { Unique = true }
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Name")
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("CreatedAt")
            )
        };
        await userCollection.Indexes.CreateManyAsync(userIndexes);
        Console.WriteLine("Users 컬렉션 생성 완료");

        // Companies 컬렉션 생성
        Console.WriteLine("Companies 컬렉션 생성 시작");
        var companyCollection = Database.GetCollection<BsonDocument>("companies");
        
        // Companies 컬렉션 인덱스 생성
        var companyIndexes = new List<CreateIndexModel<BsonDocument>>
        {
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Id"),
                new CreateIndexOptions { Unique = true }
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Name")
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("UserId")
            ),
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("CreatedAt")
            )
        };
        await companyCollection.Indexes.CreateManyAsync(companyIndexes);
        Console.WriteLine("Companies 컬렉션 생성 완료");
        
        Console.WriteLine("마이그레이션 완료: V0_0_0_CreateInitialUserAndCompanyCollectionsMigration");
    }

    public override async Task Down()
    {
        Console.WriteLine("마이그레이션 롤백 시작: V0_0_0_CreateInitialUserAndCompanyCollectionsMigration");
        await Database.DropCollectionAsync("users");
        await Database.DropCollectionAsync("companies");
        Console.WriteLine("마이그레이션 롤백 완료: V0_0_0_CreateInitialUserAndCompanyCollectionsMigration");
    }
}
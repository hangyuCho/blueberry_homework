using MongoDB.Bson;
using MongoDB.Driver;

namespace BlueberryHomeworkApp.Infrastructure.Migrations;

public class V0_0_2_UpdateUserAndCompanyFieldsMigration : MigrationBase
{
    private readonly IMongoDatabase _database;
    
    public V0_0_2_UpdateUserAndCompanyFieldsMigration(IMongoDatabase database) : base(database)
    {
        _database = database;
    }

    public override string Version => "0.0.2";

    public override async Task Up()
    {
        // User 컬렉션 업데이트
        var userCollection = _database.GetCollection<BsonDocument>("user");
        var userUpdate = Builders<BsonDocument>.Update
            .SetOnInsert("Email", new BsonDocument("$concat", new BsonArray { "$Id", "@temp.com" }));

        await userCollection.UpdateManyAsync(
            new BsonDocument("Email", new BsonDocument("$exists", false)),
            userUpdate
        );

        // Company 컬렉션 업데이트
        var companyCollection = _database.GetCollection<BsonDocument>("company");
        var companyUpdate = Builders<BsonDocument>.Update
            .SetOnInsert("Address", "N/A")
            .SetOnInsert("IsActive", true);

        await companyCollection.UpdateManyAsync(
            new BsonDocument("$or", new BsonArray
            {
                new BsonDocument("Address", new BsonDocument("$exists", false)),
                new BsonDocument("IsActive", new BsonDocument("$exists", false))
            }),
            companyUpdate
        );
    }

    public override Task Down()
    {
        return Task.CompletedTask;  // 롤백 불필요
    }
} 
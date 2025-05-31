using MongoDB.Bson;
using MongoDB.Driver;

namespace BlueberryHomeworkApp.Infrastructure.Migrations;

public class V0_0_3_RemoveUserIdFromCompanyMigration : MigrationBase
{
    private readonly IMongoDatabase _database;
    
    public V0_0_3_RemoveUserIdFromCompanyMigration(IMongoDatabase database) : base(database)
    {
        _database = database;
    }

    public override string Version => "0.0.3";

    public override async Task Up()
    {
        // Company 컬렉션에서 UserId 필드 제거
        var companyCollection = _database.GetCollection<BsonDocument>("company");
        var update = Builders<BsonDocument>.Update.Unset("UserId");

        await companyCollection.UpdateManyAsync(
            new BsonDocument("UserId", new BsonDocument("$exists", true)),
            update
        );
    }

    public override Task Down()
    {
        return Task.CompletedTask;  // 롤백 불필요
    }
} 
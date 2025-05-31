using MongoDB.Bson;
using MongoDB.Driver;

namespace BlueberryHomeworkApp.Infrastructure.Migrations;

public class V0_0_1_CleanupCompanyUserFieldMigration : MigrationBase
{
    private readonly IMongoDatabase _database;
    public V0_0_1_CleanupCompanyUserFieldMigration(IMongoDatabase database) : base(database)
    {
        _database = database;
    }

    public override string Version { get; } = "0.0.1";
    public override async Task Up()
    {
        var collection = _database.GetCollection<BsonDocument>("company");
        var update = Builders<BsonDocument>.Update.Unset("User");
        await collection.UpdateManyAsync(new BsonDocument(), update);
    }

    public override Task Down()
    {
        return Task.CompletedTask;
    }
}
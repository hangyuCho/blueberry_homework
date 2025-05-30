using MongoDB.Driver;

namespace BlueberryHomeworkApp.Infrastructure.Migrations;

public abstract class MigrationBase
{
    protected readonly IMongoDatabase Database;

    protected MigrationBase(IMongoDatabase database)
    {
        Database = database;
    }

    public abstract string Version { get; }

    public abstract Task Up();

    public abstract Task Down();
}
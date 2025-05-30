using MongoDB.Bson;
using MongoDB.Driver;

namespace BlueberryHomeworkApp.Infrastructure.Migrations;

public class MigrationManager
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<BsonDocument> _migrationCollection;
    private readonly List<MigrationBase> _migrations;

    public MigrationManager(IMongoDatabase database)
    {
        _database = database;
        _migrationCollection = _database.GetCollection<BsonDocument>("migrations");
        _migrations = new List<MigrationBase>
        {
            new V0_0_0_CreateInitialUserAndCompanyCollectionsMigration(_database)
        };
    }

    public async Task MigrateAsync()
    {
        // 마이그레이션 컬렉션이 없으면 생성
        if (!await CollectionExistsAsync("migrations"))
        {
            Console.WriteLine("migrations 컬렉션이 없어 생성합니다.");
            await _database.CreateCollectionAsync("migrations");
        }

        // 실행된 마이그레이션 목록 조회
        var executedMigrations = await _migrationCollection
            .Find(new BsonDocument())
            .Project<BsonDocument>(Builders<BsonDocument>.Projection.Include("Version"))
            .ToListAsync();

        var executedVersions = executedMigrations
            .Select(m => m["Version"].AsString)
            .ToList();

        Console.WriteLine($"실행된 마이그레이션 버전: {string.Join(", ", executedVersions)}");

        // 마이그레이션 실행
        foreach (var migration in _migrations.OrderBy(m => m.Version))
        {
            if (!executedVersions.Contains(migration.Version))
            {
                Console.WriteLine($"마이그레이션 실행: {migration.Version}");
                await migration.Up();

                await _migrationCollection.InsertOneAsync(new BsonDocument
                {
                    { "Version", migration.Version },
                    { "AppliedAt", DateTime.UtcNow }
                });

                Console.WriteLine($"마이그레이션 완료: {migration.Version}");
            }
            else
            {
                Console.WriteLine($"마이그레이션 이미 실행됨: {migration.Version}");
            }
        }
    }

    private async Task<bool> CollectionExistsAsync(string collectionName)
    {
        var filter = new BsonDocument("name", collectionName);
        var collections = await _database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
        return await collections.AnyAsync();
    }
}
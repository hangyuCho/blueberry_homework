using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlueberryHomeworkApp.Infrastructure.Migrations;

public class MigrationDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string? Version { get; set; }
    public DateTime? AppliedAt { get; set; }
}
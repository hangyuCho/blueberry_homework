using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlueberryHomeworkApp.Infrastructure.Migrations;

public class MigrationDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string? Version { get; set; }
    public DateTime? AppliedAt { get; set; }
}
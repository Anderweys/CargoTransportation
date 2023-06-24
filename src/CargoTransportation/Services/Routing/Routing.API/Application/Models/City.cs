using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Routing.API.Application.Models;

public class City
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public float Distance { get; set; }

    public City()
    {
    }

    public City(string name, float distance)
    {
        Name = name;
        Distance = distance;
    }
}

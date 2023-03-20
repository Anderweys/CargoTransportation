using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Routing.API.Models.DTOs;

namespace Routing.API.Models;

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
        Name=name;
        Distance=distance;
    }
}

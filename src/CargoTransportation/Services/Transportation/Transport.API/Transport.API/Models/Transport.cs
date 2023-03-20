using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Transportation.API.Models;

public class Transport
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; init; }
    public string Type { get; init; }
    public float AverageSpeed { get; init; }
    private TransportSize Size { get; init; }

    public Transport(string name, string type, float averageSpeed, float lenght, float height, float width)
    {
        Name = name;
        Type = type;
        AverageSpeed = averageSpeed;
        Size = new TransportSize(lenght, height, width);
    }

    public int CalculatePlaceCargo(float cargoVolume)
        => (int)Math.Ceiling((double)(cargoVolume / Size.Volume));

    public float GetVolume() => Size.Volume;
}

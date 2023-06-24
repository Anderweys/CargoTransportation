namespace Transportation.API.Application.Models;

public class TransportSize
{
    public float Length { get; set; }
    public float Height { get; set; }
    public float Width { get; set; }
    public float Volume { get; set; }

    public TransportSize(float length, float height, float width)
    {
        Length = length;
        Height = height;
        Width = width;
        Volume = length * height * width;
    }
}

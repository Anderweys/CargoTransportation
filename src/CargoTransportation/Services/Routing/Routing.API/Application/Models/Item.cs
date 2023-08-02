namespace Routing.API.Application.Models;

public class Item
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public float Length { get; init; }
    public float Width { get; init; }
    public float Height { get; init; }
    public float Price { get; init; }

    public Item(
        Guid id, string name, string description,
        float length, float width, float height, float price)
    {
        Id = id;
        Name = name;
        Description = description;
        Length = length;
        Width = width;
        Height = height;
        Price = price;
    }

    public static IEnumerable<ItemInfo> ParseToInfo(IEnumerable<Item> items)
    {
        var itemsList = new List<ItemInfo>();
        foreach (var item in items)
        {
            itemsList.Add(
                new(Name: item.Name,
                    Description: item.Description));
        }
        return itemsList;
    }
}

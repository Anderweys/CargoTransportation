using Routing.API.Application.Models;

/// <summary>
///                          DISCLAIMER:
///                          
/// To create integration events, the Masstransit library is used. 
/// To create a consumer (or handler in our case) you need an event 
/// (or contract) to create an event or handle it. But if we have 2 microservices,
/// in one of which an event is published, and the other must process it,
/// then even having a duplicate of the event in another microservice will not work,
/// because Masstransit for each handler uses its own contract within the assembly.
/// Therefore, if you need to describe an integration event,
/// then you must either have the contract have the same namespace
/// in different microservices(i.e.they are like copies),
/// or describe everything in one library and connect it for each microservice.
/// 
/// </summary>
namespace Contracts.IntegrationEvents;

public class CreateCargoIntegrationEvent
{
    public string UserId { get; init; }
    public string City { get; init; }
    public DateTime Time { get; init; }
    public float Money { get; init; }
    public IEnumerable<Item> Items { get; init; }

    public CreateCargoIntegrationEvent(string userId, string city, DateTime time, float money, IEnumerable<Item> items)
    {
        UserId=userId;
        City=city;
        Time=time;
        Money=money;
        Items=items;
    }
}

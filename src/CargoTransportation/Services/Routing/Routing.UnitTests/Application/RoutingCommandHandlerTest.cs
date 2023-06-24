using Routing.API.Application.Models.DTOs;
using Routing.API.Application.Queries;

namespace Transportation.UnitTests.Application;

public class TransportCommandHandlerTest
{
    [Fact]
    public void Handler_command_load_cities_name_when_not_null()
    {
        // Arrange.
        var mediator = new Mock<IMediator>();
        var command = new LoadCitiesQuery();

        mediator
            .Setup(m => m
                .Send(It.IsAny<IRequest<IEnumerable<CityNameDTO>>>(), new CancellationToken()))
            .Returns(Task.FromResult<IEnumerable<CityNameDTO>>(GetCityNames()));

        // Act.
        var items = mediator.Object.Send(command).Result;

        // Assert.
        Assert.NotNull(items);
    }

    private List<CityNameDTO> GetCityNames()
        => new()
        {
            new("One"),
            new("Two"),
            new("Three")
        };
}

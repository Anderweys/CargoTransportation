using Transportation.API.Application.Models.DTOs;
using Transportation.API.Application.Queries.Query;

namespace Transportation.UnitTests.Application;

public class TransportCommandQueryHandlerTest
{
    [Fact]
    public async Task Handler_query_get_transport_type_when_types_is_not_null()
    {
        // Arrange.
        var mediator = new Mock<IMediator>();
        var command = new GetTransportTypeQuery();

        mediator
            .Setup(m => m
                .Send(It.IsAny<IRequest<IEnumerable<TransportTypeDTO>>>(), new CancellationToken()))
            .Returns(Task.FromResult<IEnumerable<TransportTypeDTO>>(GetTransportTypes()));

        // Act.
        var types = mediator.Object.Send(command);

        // Assert.
        Assert.NotNull(types);
    }

    [Fact]
    public void Handler_command_get_user_items_when_it_is_null()
    {
        // Arrange.
        var mediator = new Mock<IMediator>();
        var command = new GetUserItemsQuery();

        mediator
            .Setup(m => m
                .Send(It.IsAny<IRequest<IEnumerable<ItemDTO>>>(), new CancellationToken()))
            .Returns(Task.FromResult<IEnumerable<ItemDTO>>(null));

        // Act.
        var items = mediator.Object.Send(command);

        // Assert.
        Assert.Null(items.Result);
    }

    // Create items and convert to json for emulate data from Redis.
    private List<TransportTypeDTO> GetTransportTypes()
        => new()
        {
            new("One", "LOL"),
            new("Two", "KEK"),
            new("Three", "LMAO")
        };
}

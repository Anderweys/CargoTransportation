using CargoObject.Domain.Events;

namespace CargoObject.UnitTests.Domain;


public class CargoAggregateTest
{
    public CargoAggregateTest()
    {
    }

    [Fact]
    public void Create_cargo_success()
    {
        // Arrange.
        var id = 321;
        var loaderId = 421;
        var name = "SomeGoods";
        var description = "It's a good goods!";
        var cargoTypeName = "Transport";
        var size = 100;
        var length = 20;
        var width = 10;
        var height = 4;
        var maxTemperature = 100;
        var minTemperature = -40;
        var maxPressure = 50;
        var minPressure = -25;

        // Act.
        var fakeCargo = new Cargo(id, loaderId, name, description, cargoTypeName,
                                  size, length, width, height, maxTemperature,
                                  minTemperature, maxPressure, minPressure);

        // Assert.
        Assert.NotNull(fakeCargo);
    }

    [Fact]
    public void Item_placed_into_cargo_success()
    {
        // Arrange.
        var id = 321;
        var loaderId = 421;
        var name = "SomeGoods";
        var description = "It's a good goods!";
        var cargoTypeName = "Transport";
        var size = 100;
        var length = 20;
        var width = 10;
        var height = 4;
        var maxTemperature = 100;
        var minTemperature = -40;
        var maxPressure = 50;
        var minPressure = -25;

        var itemName = "T-Short";
        var itemDescription = "type=T-Short, color=white, size=51, consist=syntetics";
        var volume = 0.5f;
        var price = 109;

        var fakeCargo = new Cargo(id, loaderId, name, description, cargoTypeName,
                                  size, length, width, height, maxTemperature,
                                  minTemperature, maxPressure, minPressure);

        // Act.
        var fakeItem = new CargoItem(itemName, itemDescription, volume, price);
        fakeCargo.PlaceCargoItem(fakeItem);
        var createFakeItem = fakeCargo.CargoItems()[0];

        // Assert.
        Assert.NotNull(createFakeItem);
    }

    [Fact]
    public void Item_placed_with_large_size_than_cargo_size_fail()
    {
        // Arrange.
        var id = 321;
        var loaderId = 421;
        var name = "SomeGoods";
        var description = "It's a good goods!";
        var cargoTypeName = "Transport";
        var size = 100;
        var length = 20;
        var width = 10;
        var height = 4;
        var maxTemperature = 100;
        var minTemperature = -40;
        var maxPressure = 50;
        var minPressure = -25;

        var itemName = "T-Short";
        var itemDescription = "type=T-Short, color=white, size=51, consist=syntetics";
        var volume = 100000000f;
        var price = 109;

        var expectedResult = 0;

        var fakeCargo = new Cargo(id, loaderId, name, description, cargoTypeName,
                                  size, length, width, height, maxTemperature,
                                  minTemperature, maxPressure, minPressure);

        // Act.
        var fakeItem = new CargoItem(itemName, itemDescription, volume, price);
        fakeCargo.PlaceCargoItem(fakeItem);

        // Assert.
        Assert.Equal(fakeCargo.CargoItems().Count, expectedResult);
    }

    [Fact]
    public void Remove_item_from_cargo_success()
    {
        // Arrange.
        var id = 321;
        var loaderId = 421;
        var name = "SomeGoods";
        var description = "It's a good goods!";
        var cargoTypeName = "Transport";
        var size = 100;
        var length = 20;
        var width = 10;
        var height = 4;
        var maxTemperature = 100;
        var minTemperature = -40;
        var maxPressure = 50;
        var minPressure = -25;

        var itemName = "T-Short";
        var itemDescription = "type=T-Short, color=white, size=51, consist=syntetics";
        var volume = 1f;
        var price = 109;

        var expectedResult = 0;

        var fakeCargo = new Cargo(id, loaderId, name, description, cargoTypeName,
                                  size, length, width, height, maxTemperature,
                                  minTemperature, maxPressure, minPressure);
        var fakeItem = new CargoItem(itemName, itemDescription, volume, price);
        fakeCargo.PlaceCargoItem(fakeItem);

        // Act.
        fakeCargo.RemoveCargoItemById(0);

        // Assert.
        Assert.Equal(fakeCargo.CargoItems().Count, expectedResult);
    }

    [Fact]
    public void Load_cargo_success()
    {
        // Arrange.
        var id = 321;
        var loaderId = 421;
        var name = "SomeGoods";
        var description = "It's a good goods!";
        var cargoTypeName = "Transport";
        var size = 100;
        var length = 20;
        var width = 10;
        var height = 4;
        var maxTemperature = 100;
        var minTemperature = -40;
        var maxPressure = 50;
        var minPressure = -25;

        var expectedResult = 1;

        // Act.
        var fakeCargo = new Cargo(id, loaderId, name, description, cargoTypeName,
                                  size, length, width, height, maxTemperature,
                                  minTemperature, maxPressure, minPressure);
        fakeCargo.AddDomainEvent(new CargoPlacedDomainEvent(
                loaderId,
                $"A new cargo with items prepared for loading.\n" +
                $"LoaderId: {loaderId}\nId: {id}",
                fakeCargo,
                DateTime.UtcNow));

        // Assert.
        Assert.Equal(fakeCargo.DomainEvents.Count, expectedResult);
    }
}

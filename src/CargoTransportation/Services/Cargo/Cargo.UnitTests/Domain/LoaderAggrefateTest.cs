namespace CargoObject.UnitTests.Domain;

public class LoaderAggrefateTest
{
	public LoaderAggrefateTest()
	{
	}

	[Fact]
	public void Load_one_cargo_success()
	{
		// Arrange.
		var cargo = new Cargo();
		var loader = new Loader();

		// Act.
		loader.LoadCargo(cargo);

		// Assert.
		Assert.NotNull(loader.Cargos());
	}

	[Fact]
	public void Load_cargo_collection_success()
	{
		// Arrange.
		var cargos = new Cargo[]{
			new Cargo(),
			new Cargo(),
			new Cargo()
		};
		var loader = new Loader();
		var exceptedResult = 3;

		// Act.
		loader.LoadCargos(cargos);

		// Assert.
		Assert.Equal(loader.Cargos().Count, exceptedResult);
	}

	[Fact]
	public void Remove_cargo_by_loader_success()
	{
		// Arrange.
		var loader = new Loader();

		// Act.
		loader.LoadCargo(new());
		loader.UnloadCargo(new());

		// Assert.
		Assert.NotNull(loader.Cargos());
	}

	[Fact]
	public void Finish_load_cargos_and_prepare_to_transport_success()
	{
        // Arrange.
        var cargos = new Cargo[]{
            new Cargo(),
            new Cargo(),
            new Cargo()
        };
        var loader = new Loader();
        var exceptedResult = 1;

        // Act.
        loader.LoadCargos(cargos);
		loader.FinishLoad();

        // Assert.
        Assert.Equal(loader.DomainEvents.Count, exceptedResult);
    }
}

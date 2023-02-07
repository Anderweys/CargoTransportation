using CargoObject = Cargo.Domain.AggregatesModel.CargoAggregates.Cargo;
using Cargo.Domain.SeedWork;

namespace Cargo.Domain.AggregatesModel.LoaderAggragates;


public class Loader : Entity, IAggregateRoot
{
    public Bio Bio { get; private set; }
    public string Description { get; private set; }
    public DateTime LoadingTime { get; private set; }
    private List<CargoObject> _cargos;

    public Loader()
    {
        _cargos = new();
    }

    public Loader(string lastName, string firstName, string middleName, short age,
        string description, DateTime loadingTime)
    {
        Bio = new(lastName, firstName, middleName, age);
        Description = description;
        LoadingTime = loadingTime;
        _cargos = new();
    }

    public void LoadCargo(CargoObject cargo)
    {
        _cargos.Add(cargo);
    }
    public void LoadCargos(IEnumerable<CargoObject> cargos)
    {
        foreach(var cargo in cargos)
        {
            _cargos.Add(cargo);
        }
    }
    public void UnloadCargo(CargoObject cargo)
    {
        _cargos.Remove(cargo);
    }

    public void FinishLoad()
    {

    }
}

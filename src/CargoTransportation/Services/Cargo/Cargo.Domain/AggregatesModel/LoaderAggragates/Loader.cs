using CargoObject.Domain.AggregatesModel.CargoAggregates;
using CargoObject.Domain.Events;
using CargoObject.Domain.SeedWork;
using System.Collections;

namespace CargoObject.Domain.AggregatesModel.LoaderAggragates;


public class Loader : Entity, IAggregateRoot
{
    public Bio Bio { get; private set; }
    public string Description { get; private set; }
    public DateTime LoadingTime { get; private set; }
    private List<Cargo> _cargos;

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

    public IReadOnlyCollection<Cargo> Cargos() => _cargos.AsReadOnly();
    public void LoadCargo(Cargo cargo)
    {
        _cargos.Add(cargo);
    }
    public void LoadCargos(IEnumerable<Cargo> cargos)
    {
        foreach (var cargo in cargos)
        {
            _cargos.Add(cargo);
        }
    }
    public void UnloadCargo(Cargo cargo)
    {
        _cargos.Remove(cargo);
    }
    public void FinishLoad()
    {
        AddDomainEvent(new LoadFinishedDomainEvent(
            Id,
            _cargos,
            DateTime.UtcNow));
    }
}

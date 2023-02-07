using CargoObject = Cargo.Domain.AggregatesModel.CargoAggregates.Cargo;
using MediatR;

namespace Cargo.Domain.Events;


public class LoadFinishedDomainEvent : INotification
{
    public int LoaderId { get; }
    public List<CargoObject> Cargos { get; }
    public DateTime FinishTime { get; }

    public LoadFinishedDomainEvent(int loaderId, List<CargoObject> cargos, DateTime finishTime)
    {
        LoaderId = loaderId;
        Cargos = cargos;
        FinishTime = finishTime;
    }
}

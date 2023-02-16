using CargoObject.Domain.AggregatesModel.CargoAggregates;
using MediatR;

namespace CargoObject.Domain.Events;


public class LoadFinishedDomainEvent : INotification
{
    public int LoaderId { get; }
    public List<Cargo> Cargos { get; }
    public DateTime FinishTime { get; }

    public LoadFinishedDomainEvent(int loaderId, List<Cargo> cargos, DateTime finishTime)
    {
        LoaderId = loaderId;
        Cargos = cargos;
        FinishTime = finishTime;
    }
}

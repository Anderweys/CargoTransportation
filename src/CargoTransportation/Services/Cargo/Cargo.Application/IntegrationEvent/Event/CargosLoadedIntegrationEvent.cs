using CargoObject = Cargo.Domain.AggregatesModel.CargoAggregates.Cargo;
using Cargo.Domain.AggregatesModel.LoaderAggragates;

namespace Cargo.Application.IntegrationEvent.Event;


public class CargosLoadedIntegrationEvent
{
    public Loader Loader { get; }
    public List<CargoObject> Cargos { get; }

    public CargosLoadedIntegrationEvent(Loader loader, List<CargoObject> cargos)
    {
        Loader=loader;
        Cargos=cargos;
    }
}

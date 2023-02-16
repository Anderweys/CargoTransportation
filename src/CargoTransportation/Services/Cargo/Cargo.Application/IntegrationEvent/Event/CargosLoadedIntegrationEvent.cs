using CargoObject = CargoObject.Domain.AggregatesModel.CargoAggregates.Cargo;
using CargoObject.Domain.AggregatesModel.LoaderAggragates;

namespace CargoObject.Application.IntegrationEvent.Event;


public class CargosLoadedIntegrationEvent
{
    public Loader Loader { get; }
    public List<Domain.AggregatesModel.CargoAggregates.Cargo> Cargos { get; }

    public CargosLoadedIntegrationEvent(Loader loader, List<Domain.AggregatesModel.CargoAggregates.Cargo> cargos)
    {
        Loader=loader;
        Cargos=cargos;
    }
}

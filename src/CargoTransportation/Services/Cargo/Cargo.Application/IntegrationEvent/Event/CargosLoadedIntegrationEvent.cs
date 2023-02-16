using CargoObject.Domain.AggregatesModel.CargoAggregates;
using CargoObject.Domain.AggregatesModel.LoaderAggragates;

namespace CargoObject.Application.IntegrationEvent.Event;


public class CargosLoadedIntegrationEvent
{
    public Loader Loader { get; }
    public List<Cargo> Cargos { get; }

    public CargosLoadedIntegrationEvent(Loader loader, List<Cargo> cargos)
    {
        Loader=loader;
        Cargos=cargos;
    }
}

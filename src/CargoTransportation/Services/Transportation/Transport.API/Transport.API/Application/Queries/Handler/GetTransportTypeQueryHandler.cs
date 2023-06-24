using MediatR;
using Transportation.API.Application.Models;
using Transportation.API.Application.Models.DTOs;
using Transportation.API.Application.Queries.Query;

namespace Transportation.API.Application.Queries.Handler;

public class GetTransportTypeQueryHandler : IRequestHandler<GetTransportTypeQuery, IEnumerable<TransportTypeDTO>>
{
    private readonly ITransportRepository _transportRepository;

    public GetTransportTypeQueryHandler(ITransportRepository transportRepository)
    {
        _transportRepository = transportRepository ?? throw new ArgumentNullException(nameof(transportRepository));
    }

    public async Task<IEnumerable<TransportTypeDTO>> Handle(GetTransportTypeQuery request, CancellationToken cancellationToken)
    {
        var transports = await _transportRepository.GetAllAsync();

        List<TransportTypeDTO> result = new();

        foreach (var item in transports)
        {
            result.Add(new(item.Name, item.Type));
        }

        return result;
    }
}

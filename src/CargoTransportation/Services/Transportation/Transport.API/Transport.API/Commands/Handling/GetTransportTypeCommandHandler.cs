using MediatR;
using Transportation.API.Commands.Command;
using Transportation.API.Models;
using Transportation.API.Models.DTOs;

namespace Transportation.API.Commands.Handling;

public class GetTransportTypeCommandHandler : IRequestHandler<GetTransportTypeCommand, IEnumerable<TransportTypeDTO>>
{
    private readonly ITransportRepository _transportRepository;

    public GetTransportTypeCommandHandler(ITransportRepository transportRepository)
    {
        _transportRepository = transportRepository ?? throw new ArgumentNullException(nameof(transportRepository));
    }

    public async Task<IEnumerable<TransportTypeDTO>> Handle(GetTransportTypeCommand request, CancellationToken cancellationToken)
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

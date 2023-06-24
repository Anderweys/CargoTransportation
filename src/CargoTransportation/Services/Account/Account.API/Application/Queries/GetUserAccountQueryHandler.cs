using MediatR;
using Account.API.Domain.Entities;
using Account.API.Domain.Interface;

namespace Account.API.Application.Queries;

public class GetUserAccountQueryHandler : IRequestHandler<GetUserAccountQuery, User?>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<GetUserAccountQueryHandler> _logger;

    public GetUserAccountQueryHandler(IAccountRepository accountRepository, ILogger<GetUserAccountQueryHandler> logger)
    {
        _accountRepository=accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<User?> Handle(GetUserAccountQuery request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Login = request.Login,
            Password = request.Password
        };

        User? account;

        try
        {
            account = await _accountRepository.GetAsync(user);

            return account;
        }
        catch (OperationCanceledException)
        {
            _logger.LogError(
               "Suddenness thread was canceled. Handler: {handler}, time: {time}.",
               nameof(GetUserAccountQueryHandler),
               DateTime.UtcNow.ToLongDateString());

            account = null;
        }

        return account;
    }
}

using MediatR;
using Account.API.Domain.Interface;
using Account.API.Domain.Entities;

namespace Account.API.Application.Commands;

public class AddUserAccountCommandHandler : IRequestHandler<AddUserAccountCommand, bool>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<AddUserAccountCommandHandler> _logger;

    public AddUserAccountCommandHandler(IAccountRepository accountRepository, ILogger<AddUserAccountCommandHandler> logger)
    {
        _accountRepository=accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(AddUserAccountCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Login = request.Login,
            Password = request.Password
        };

        var isCreated = await _accountRepository.AddAsync(user);

        if (isCreated)
        {
            _logger.LogInformation(
                       "Account has been created. Command handler: {handler}, time: {time}.",
                       nameof(AddUserAccountCommandHandler),
                       DateTime.UtcNow.ToLongDateString());
            return true;
        }
        else
        {
            return false;
        }
    }
}

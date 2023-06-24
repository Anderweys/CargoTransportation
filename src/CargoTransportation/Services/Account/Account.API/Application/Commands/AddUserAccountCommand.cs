using MediatR;
using System.Runtime.Serialization;

namespace Account.API.Application.Commands;

[DataContract]
public class AddUserAccountCommand : IRequest<bool>
{
    [DataMember]
    public string Login { get; init; }
    [DataMember]
    public string Password { get; init; }

    public AddUserAccountCommand(string login, string password)
    {
        Login=login;
        Password=password;
    }
}

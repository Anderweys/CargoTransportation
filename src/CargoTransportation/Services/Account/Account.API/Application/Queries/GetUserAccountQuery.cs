using Account.API.Domain.Entities;
using MediatR;
using System.Runtime.Serialization;

namespace Account.API.Application.Queries;

[DataContract]
public class GetUserAccountQuery : IRequest<User?>
{
    [DataMember]
    public string Login { get; set; }
    [DataMember]
    public string Password { get; set; }

    public GetUserAccountQuery()
    {
    }

    public GetUserAccountQuery(string login, string password)
    {
        Login=login;
        Password=password;
    }
}

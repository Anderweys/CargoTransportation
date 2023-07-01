namespace WebMVC.Infrastructure.API;

public static partial class API
{
    public static class Account
    {
        public static string AddUser(string baseUri) => $"{baseUri}/adduser";
        public static string GetUser(string baseUri, string login, string password)
            => $"{baseUri}/getuser?login={login}&password={password}";
    }
}

﻿namespace Transportation.API.Models;

public interface ITransportRepository
{
    Task<Transport> GetAsync(string name, string type);
    Task<IEnumerable<Transport>> GetAllAsync();
}

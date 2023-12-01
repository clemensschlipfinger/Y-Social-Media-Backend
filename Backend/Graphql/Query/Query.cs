using Domain.Repositories.Interfaces;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace TestGraphql;

public class Query
{
    public IQueryable<Yeet> GetYeets([Service] IYeetRepository repo)
        => repo.ReadFullYeet();
}
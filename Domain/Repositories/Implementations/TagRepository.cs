﻿using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class TagRepository(YDbContext context) : ARepository<Tag>(context), ITagRepository
{
    public async Task<Graphql.Types.Tag?> ReadTag(int id) =>
        (await Table.FirstOrDefaultAsync(t => t.Id == id)).Adapt<Graphql.Types.Tag>();


    public async Task<List<Graphql.Types.Tag>> ReadTags() =>
        await Table.Select(t => t.Adapt<Graphql.Types.Tag>()).ToListAsync();
}
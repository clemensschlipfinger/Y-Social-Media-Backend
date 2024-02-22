using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class TagRepository : ARepository<Tag>, ITagRepository
{
    public TagRepository(IDbContextFactory<YDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }

    public async Task<TagResult> ReadTag(TagInput input)
    {
        var tag = await Table.FirstOrDefaultAsync(t => t.Id == input.TagId);
        if (tag is null) { return new TagResult(null); }
        
        return new TagResult(tag.Adapt<Graphql.Types.Tag>());
    }

    public async Task<TagsResult> ReadTags(TagsInput input)
    {
        var tagsQuery = Table.AsQueryable();

        if (input.Filter is not null)
            tagsQuery = tagsQuery.Where(u => u.Name.Contains(input.Filter));

        switch (input.Direction)
        {
            case SortDirection.ASC:
                switch (input.Sorting)
                {
                    case SortTags.NAME:
                        tagsQuery = tagsQuery.OrderBy(t => t.Name);
                        break;  
                    case SortTags.ID:
                        tagsQuery = tagsQuery.OrderBy(t => t.Id);
                        break;  
                }
                break;
            case SortDirection.DSC:
                switch (input.Sorting)
                {
                    case SortTags.NAME:
                        tagsQuery = tagsQuery.OrderByDescending(t => t.Name);
                        break;  
                    case SortTags.ID:
                        tagsQuery = tagsQuery.OrderByDescending(t => t.Id);
                        break;  
                }
                break;
        }

        tagsQuery = tagsQuery.Skip(input.Offset).Take(input.Limit);

        var count = await tagsQuery.CountAsync();
        var users = await tagsQuery.ToListAsync();
        
        return new TagsResult(users.Select(u => u.Adapt<Graphql.Types.Tag>()).ToList(), count);
    }

    public Task<List<Graphql.Types.Tag>> ReadAllGraphqlTag()
    {
        return Table.Select(u => u.Adapt<Graphql.Types.Tag>()).ToListAsync();
    }
}
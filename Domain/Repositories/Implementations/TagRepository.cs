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

        if (input.Filter is not null && input.Filter.Length > 0)
            tagsQuery = tagsQuery.Where(u => u.Name.Contains(input.Filter));

        tagsQuery = input.Direction switch
        {
            SortDirection.ASC => input.Sorting switch
            {
                SortTags.ID => tagsQuery.OrderBy(t => t.Id),
                SortTags.NAME => tagsQuery.OrderBy(t => t.Name).ThenBy(t => t.Id),
                _ => throw new ArgumentOutOfRangeException()
            },
            SortDirection.DSC => input.Sorting switch
            {
                SortTags.ID => tagsQuery.OrderByDescending(t => t.Id),
                SortTags.NAME => tagsQuery.OrderByDescending(t => t.Name).ThenByDescending(t => t.Id),
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => tagsQuery
        };

        tagsQuery = tagsQuery.Skip(input.Offset).Take(input.Limit);

        var count = await tagsQuery.CountAsync();
        var users = await tagsQuery.ToListAsync();
        
        return new TagsResult(users.Select(u => u.Adapt<Graphql.Types.Tag>()).ToList(), count);
    }

    public Task<List<Graphql.Types.Tag>> ReadAllGraphqlTag()
    {
        return Table.Select(u => u.Adapt<Graphql.Types.Tag>()).ToListAsync();
    }

    public async Task<bool> ExistByIds(List<int> ids) => await Table.Where(y => ids.Contains(y.Id)).CountAsync() == ids.Count;
    public Task<bool> Exists(int tagId) =>  Table.AnyAsync(t => t.Id == tagId);
}
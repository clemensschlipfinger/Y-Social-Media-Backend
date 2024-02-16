using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record DeleteYommentResult(Yeet Yeet, List<Yomment> Yomments);
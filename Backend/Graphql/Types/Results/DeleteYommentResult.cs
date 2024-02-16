using Model.Entities;

namespace Backend.Graphql.Types.Results;

public record DeleteYommentResult(Yeet Yeet, List<Yomment> Yomments);
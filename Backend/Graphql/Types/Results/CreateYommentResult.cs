using Model.Entities;

namespace Backend.Graphql.Types.Results;

public record CreateYommentResult(Yeet Yeet, Yomment Yomment, List<Yomment> Yomments);
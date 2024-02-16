using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record CreateYommentResult(Yeet Yeet, Yomment Yomment, List<Yomment> Yomments);
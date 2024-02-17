namespace Domain.Services.Interfaces;

public interface IRegexService
{
    bool Matches(string filter, string content);
}
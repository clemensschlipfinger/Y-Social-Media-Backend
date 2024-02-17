using System.Text.RegularExpressions;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations;

public class RegexService: IRegexService
{
    public bool Matches(string filter, string content)
    {
        return Regex.IsMatch(content, filter);
    }
}
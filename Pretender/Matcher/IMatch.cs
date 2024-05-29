using Pretender.Configuration;

namespace Pretender.Matcher;

public interface IMatch
{
    bool IsMatch(Mock mock, RequestInput requestInput);
}
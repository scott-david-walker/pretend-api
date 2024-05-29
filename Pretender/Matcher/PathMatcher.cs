using Pretender.Configuration;

namespace Pretender.Matcher;

public class PathMatcher : IMatch
{
    public bool IsMatch(Mock mock, RequestInput requestInput)
    {
        return mock.Request?.Path == requestInput.Path;
    }
}
using Pretender.Configuration;

namespace Pretender.Matcher;

public class HeaderMatcher : IMatch
{
    public bool IsMatch(Mock mock, RequestInput requestInput)
    {
        return mock.Request?.Match == null || KeyValueMatcher.IsMatch(mock.Request.Match.Headers, requestInput.Headers);
    }
}
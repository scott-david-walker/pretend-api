using Pretender.Configuration;

namespace Pretender.Matcher;

public class QueryParamsMatcher : IMatch
{
    public bool IsMatch(Mock mock, RequestInput requestInput)
    {
        return mock.Request?.Match == null || KeyValueMatcher.IsMatch(mock.Request.Match.Params, requestInput.QueryParams);
    }
}
using Pretender.Configuration;

namespace Pretender.Matcher;

public interface IMatcher
{
    Task<Mock> Match(RequestInput input);
}
public class Matcher : IMatcher
{
    private readonly Config _config;
    private readonly List<IMatch> _matchers = [new PathMatcher()];
    public Matcher(Config config)
    {
        _config = config;
    }
    
    public async Task<Mock> Match(RequestInput input)
    {
        foreach (var mock in  _config.Mocks)
        {
            foreach (var matcher in _matchers)
            {
                var result = matcher.IsMatch(mock, input);
                if (!result)
                {
                    break;
                }
            }
        }
        if (input.ContentType == "application/json")
        {
            using (var reader = new StreamReader(input.Body))
            {
                var body = await reader.ReadToEndAsync();
            }
        }

        return new Mock();
    }
}
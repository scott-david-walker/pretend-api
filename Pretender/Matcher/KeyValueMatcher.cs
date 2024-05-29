using Pretender.Configuration;

namespace Pretender.Matcher;

public static class KeyValueMatcher
{
    public static bool IsMatch(List<KeyValueMatch> mock, Dictionary<string, string> requestInput)
    {
        if (mock.Count == 0)
        {
            return true;
        }

        foreach (var m in mock)
        {
            var key = m.Key;
            if (!requestInput.ContainsKey(key))
            {
                return false;
            }

            var val = requestInput[key];
            if (val != m.Value)
            {
                return false;
            }
        }

        return true;
    }
}
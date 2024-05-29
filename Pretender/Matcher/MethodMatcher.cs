using Pretender.Configuration;

namespace Pretender.Matcher;

public class MethodMatcher : IMatch
{
    public bool IsMatch(Mock mock, RequestInput requestInput)
    {
        if (mock.Request?.Method == null)
        {
            return true;
        }

        var method = mock.Request.Method.ToUpper();
        return method == requestInput.Method;
    }
}
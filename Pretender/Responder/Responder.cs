using Pretender.Configuration;

namespace Pretender.Responder;

public interface IResponder
{
    Response CreateResponse(MockResponse mockResponse);
}

public class Responder : IResponder
{
    public Response CreateResponse(MockResponse mockResponse)
    {
        return new()
        {
            StatusCode = mockResponse.StatusCode,
            Content = mockResponse.Body?.Content
        };
    }
}
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Pretender;
using Pretender.Matcher;
using Pretender.Responder;

namespace Api;

public class HttpAnyAttribute : HttpMethodAttribute
{
    private static readonly IEnumerable<string> _supportedMethods = new[] 
    { 
        "GET", "POST", "PUT", "DELETE", "HEAD", "OPTIONS", "PATCH" 
    };

    public HttpAnyAttribute() : base(_supportedMethods)
    {
    }
}

[Route("api/[controller]")]
[ApiController]
public class AnyVerbController : ControllerBase
{
    private readonly IMatcher _matcher;
    private readonly IResponder _responder;

    public AnyVerbController(
        IMatcher matcher, 
        IResponder responder)
    {
        _matcher = matcher;
        _responder = responder;
    }
    [HttpAny]
    [Route("{*path}")]
    public async Task<IActionResult> HandleAnyRequest()
    {
        var r = HttpContext.Request;
        var request = new RequestInput
        {
            Path = HttpUtility.UrlDecode(r.Path.ToString()).Split("/api/AnyVerb").Last(),
            Headers = r.Headers.ToDictionary(x => x.Key, x => x.Value.ToString()),
            QueryParams = r.Query.ToDictionary(x => x.Key, x => x.Value.ToString()),
            ContentType = r.ContentType,
            Method = r.Method,
            Body = r.Body
        };

        var mock = await _matcher.Match(request);
        var response = _responder.CreateResponse(mock.Response);
        return new ContentResult
        {
            Content = response.Content,
            ContentType = "application/json",
            StatusCode = response.StatusCode
        };
    }
    
    [HttpPost]
    [Route("test")]
    public IActionResult HandleRequest()
    {
        // Potential config endpoint
        return Ok();
    }
}
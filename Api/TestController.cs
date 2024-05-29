using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Pretender;
using Pretender.Matcher;
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

    public AnyVerbController(IMatcher matcher)
    {
        _matcher = matcher;
    }
    [HttpAny]
    [Route("{*path}")]
    public async Task<IActionResult> HandleAnyRequest()
    {
        var r = HttpContext.Request;
        var request = new RequestInput
        {
            Path = r.Path,
            Headers = r.Headers.ToDictionary(x => x.Key, x => x.Value.ToString()),
            QueryParams = r.Query.ToDictionary(x => x.Key, x => x.Value.ToString()),
            ContentType = r.ContentType,
            Method = r.Method,
            Body = r.Body
        };

        var mock = await _matcher.Match(request);
        return Ok(mock);
    }
    
    [HttpPost]
    [Route("test")]
    public IActionResult HandleRequest()
    {
        // Potential config endpoint
        return Ok();
    }
}
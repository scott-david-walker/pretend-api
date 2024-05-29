namespace Pretender.Configuration;

public class Mock
{
    public string Name { get; set; }
    public Request? Request { get; set; }
}

public class Request
{
    public string? Path { get; set; }
    public string? Method { get; set; }
    public Match? Match { get; set; }
}

public class Match
{
    public List<KeyValueMatch> Params { get; set; } = [];
    public List<KeyValueMatch> Headers { get; set; } = [];
}

public class KeyValueMatch
{
    public string Key { get; set; } = null!;
    public string Value { get; set; }= null!;
}
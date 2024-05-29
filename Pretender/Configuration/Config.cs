namespace Pretender.Configuration;

public class Config
{
    public string Version { get; set; } = "v1";
    public List<Mock> Mocks { get; set; } = [];
}
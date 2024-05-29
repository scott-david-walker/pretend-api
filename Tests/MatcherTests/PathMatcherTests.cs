using FluentAssertions;
using Pretender;
using Pretender.Configuration;
using Pretender.Matcher;

namespace Tests.MatcherTests;

public class PathMatcherTests
{
    private readonly PathMatcher _sut = new();

    [Fact]
    public void WhenPathsAreTheSame_ShouldReturnTrue()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Path = "path"
            }
        };

        var request = new RequestInput
        {
            Path = "path"
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("/path", "path")]
    [InlineData("/Path", "/path")]
    public void WhenPathsAreNotTheSame_ShouldReturnFalse(string mockPath, string requestUrl)
    {
        var mock = new Mock
        {
            Request = new()
            {
                Path = mockPath
            }
        };

        var request = new RequestInput
        {
            Path = requestUrl
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeFalse();
    }
}
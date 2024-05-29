using FluentAssertions;
using Pretender;
using Pretender.Configuration;
using Pretender.Matcher;

namespace Tests.MatcherTests;

public class MethodMatcherTests
{
    private readonly MethodMatcher _sut = new();

    [Fact]
    public void WhenMethodsAreTheSame_ShouldReturnTrue()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Method = "POST"
            }
        };

        var request = new RequestInput
        {
            Method = "POST"
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenMockMethodIsNull_ShouldReturnTrue()
    {
        var mock = new Mock
        {
            Request = new()
        };

        var request = new RequestInput
        {
            Method = "POST"
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenMockMethodIsLowerCase_ButStillMatches_ShouldReturnTrue()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Method = "post"
            }
        };

        var request = new RequestInput
        {
            Method = "POST"
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("get", "post")]
    [InlineData("patch", "option")]
    public void WhenMethodsAreNotTheSame_ShouldReturnFalse(string mockMethod, string requestMethod)
    {
        var mock = new Mock
        {
            Request = new()
            {
                Method = mockMethod
            }
        };

        var request = new RequestInput
        {
            Method = requestMethod
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeFalse();
    }
}
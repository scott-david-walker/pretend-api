using FluentAssertions;
using Pretender;
using Pretender.Configuration;
using Pretender.Matcher;

namespace Tests.MatcherTests;

public class HeaderMatcherTests
{
    private readonly HeaderMatcher _sut = new();

    [Fact]
    public void WhenMatchObjectIsNull_ShouldReturnTrue()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Match = null
            }
        };

        var result = _sut.IsMatch(mock, new());
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenMatchHeadersAreEmpty_ShouldReturnTrue()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Match = new()
            }
        };

        var result = _sut.IsMatch(mock, new());
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenMatchHeadersExistAndKeyAndValueMatch_ShouldReturnTrue()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Match = new()
                {
                    Headers = [new() {Key = "key", Value = "value"}]
                }
            }
        };

        var request = new RequestInput()
        {
            Headers = new()
            {
                { "key", "value" }
            }
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenMatchHeadersExist_AndMockKeyIsNotInRequest_ShouldReturnFalse()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Match = new()
                {
                    Headers = [new() {Key = "key", Value = "not matching value"}]
                }
            }
        };

        var request = new RequestInput()
        {
            Headers = new()
            {
                { "key", "value" }
            }
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenMatchHeadersExist_AndKeyMatches_ButValueDoesNotMatch_ShouldReturnFalse()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Match = new()
                {
                    Headers = [new() {Key = "key", Value = "not matching value"}]
                }
            }
        };

        var request = new RequestInput()
        {
            Headers = new()
            {
                { "key", "value" }
            }
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenMatchHeadersExist_AndOneKeyMatches_ButOneDoesNot_ShouldReturnFalse()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Match = new()
                {
                    Headers = [new() {Key = "match", Value = "match"}, new() {Key = "not match", Value = "not match"}]
                }
            }
        };

        var request = new RequestInput
        {
            Headers = new()
            {
                { "match", "match" },
                { "not match", "not matching" }
            }
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenMultipleHeadersMatch_ShouldReturnTrue()
    {
        var mock = new Mock
        {
            Request = new()
            {
                Match = new()
                {
                    Headers = [new() {Key = "match", Value = "match"}, new() {Key = "another match", Value = "another match"}]
                    
                }
            }
        };

        var request = new RequestInput
        {
            Headers = new()
            {
                { "match", "match" },
                { "another match", "another match" }
            }
        };

        var result = _sut.IsMatch(mock, request);
        result.Should().BeTrue();
    }
}
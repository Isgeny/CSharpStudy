using System.Linq.Expressions;
using FluentAssertions;
using Xunit;

namespace CSharpStudy.Expressions;

public class ExpressionsInQueryableTests
{
    [Fact]
    public void RuntimeState()
    {
        var names = new[] { "Foo", "Bar", "Baz" };

        var length = 1;
        var qry = names
            .AsQueryable()
            .Select(x => x.Substring(0, length));

        string.Join(",", qry).Should().Be("F,B,B");

        length = 2;

        string.Join(",", qry).Should().Be("Fo,Ba,Ba");
    }

    [Fact]
    public void CallAdditionalLinqMethod()
    {
        var queryable = new[] { "A", "BBB", "CC" }.AsQueryable();
        var sortByLength = true;
        if (sortByLength)
        {
            queryable = queryable.OrderBy(x => x.Length);
        }

        queryable.Should().BeEquivalentTo("A", "CC", "BBB");
    }

    [Fact]
    public void VaryExpressions()
    {
        var queryable = new[] { "Foo", "Bar", "Baz" }.AsQueryable();
        var startsWith = "F";
        var endsWith = "C";

        Expression<Func<string, bool>> expr = (startsWith, endsWith) switch
        {
            ("" or null, "" or null) => x => true,
            (_, "" or null) => x => x.StartsWith(startsWith),
            ("" or null, _) => x => x.EndsWith(endsWith),
            (_, _) => x => x.StartsWith(startsWith) || x.EndsWith(endsWith)
        };

        var result = queryable.Where(expr);

        result.Should().BeEquivalentTo("Foo");
    }
}
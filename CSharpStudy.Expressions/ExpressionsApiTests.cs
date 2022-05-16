using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace CSharpStudy.Expressions;

public class ExpressionsApiTests
{
    [Fact]
    public void Factorial()
    {
        var value = Expression.Parameter(typeof(int), "value");
        var result = Expression.Parameter(typeof(int), "result");
        var label = Expression.Label(typeof(int));

        var block = Expression.Block(
            new[] { result },
            Expression.Assign(result, Expression.Constant(1)),
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.GreaterThan(value, Expression.Constant(1)),
                    Expression.MultiplyAssign(result,
                        Expression.PostDecrementAssign(value)),
                    Expression.Break(label, result)
                ),
                label
            )
        );

        var lambda = Expression.Lambda<Func<int, int>>(block, value);
        var factorial = lambda.Compile()(5);

        using (new AssertionScope())
        {
            lambda.ToReadableString().Should().Be(@"value =>
{
    var result = 1;
    while (true)
    {
        if (value > 1)
        {
            result *= value--;
        }
        else
        {
            break;
        }
    }
}");

            factorial.Should().Be(120);
        }
    }
}
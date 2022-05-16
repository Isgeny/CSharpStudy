using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace CSharpStudy.Expressions;

public class LambdaExpressionTests
{
    [Fact]
    public void SingleLineExpression()
    {
        Expression<Func<int, bool>> lambda = num => num < 5;

        lambda.ToReadableString().Should().Be("num => num < 5");
    }

    [Fact]
    public void ExpressionManually()
    {
        var numParam = Expression.Parameter(typeof(int), "num");
        var five = Expression.Constant(5, typeof(int));
        var numLessThanFive = Expression.LessThan(numParam, five);
        var lambda = Expression.Lambda<Func<int, bool>>(numLessThanFive, numParam);

        lambda.ToReadableString().Should().Be("num => num < 5");
    }

    [Fact]
    public void DecomposeLambda()
    {
        Expression<Func<int, bool>> lambda = num => num < 5;

        var numParam = lambda.Parameters[0];
        var operation = (BinaryExpression)lambda.Body;
        var left = (ParameterExpression)operation.Left;
        var right = (ConstantExpression)operation.Right;

        using (new AssertionScope())
        {
            numParam.ToReadableString().Should().Be("num");
            operation.ToReadableString().Should().Be("num < 5");
            left.ToReadableString().Should().Be("num");
            right.ToReadableString().Should().Be("5");
        }
    }
}
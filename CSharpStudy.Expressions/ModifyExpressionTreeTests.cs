using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using FluentAssertions;
using Xunit;

namespace CSharpStudy.Expressions;

public class AndAlsoModifier : ExpressionVisitor
{
    public Expression Modify(Expression expression)
    {
        return Visit(expression);
    }

    protected override Expression VisitBinary(BinaryExpression b)
    {
        if (b.NodeType == ExpressionType.AndAlso)
        {
            var left = Visit(b.Left);
            var right = Visit(b.Right);
            return Expression.MakeBinary(ExpressionType.OrElse, left, right, b.IsLiftedToNull, b.Method);
        }

        return base.VisitBinary(b);
    }
}

public class ModifyExpressionTreeTests
{
    [Fact]
    public void ChangeAndToOr()
    {
        Expression<Func<string, bool>> lambda = name => name.Length > 10 && name.StartsWith("G");

        var treeModifier = new AndAlsoModifier();
        var modifiedExpression = treeModifier.Modify(lambda);

        modifiedExpression.ToReadableString().Should().Be("name => (name.Length > 10) || name.StartsWith(\"G\")");
    }
}
using Xunit;

namespace UnitTesting.Tests;

public class MathHelperTests
{
    private readonly MathHelper _math = new MathHelper();

    [Fact]
    public void Add_ReturnsCorrectSum()
    {
        double result = _math.Add(3, 4);
        Assert.Equal(7, result);
    }

    [Fact]
    public void Add_WithNegativeNumbers_ReturnsCorrectSum()
    {
        double result = _math.Add(-2, -3);
        Assert.Equal(-5, result);
    }

    [Fact]
    public void Divide_ReturnsCorrectQuotient()
    {
        double result = _math.Divide(10, 2);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => _math.Divide(5, 0));
    }
}

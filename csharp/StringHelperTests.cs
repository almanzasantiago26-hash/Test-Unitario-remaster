using Xunit;

namespace UnitTesting.Tests;

public class StringHelperTests
{
    private readonly StringHelper _helper = new StringHelper();

    // --- truncate ---

    [Fact]
    public void Truncate_TextShorterThanLimit_ReturnsOriginal()
    {
        Assert.Equal("Hola", _helper.Truncate("Hola", 10));
    }

    [Fact]
    public void Truncate_TextExactlyAtLimit_ReturnsOriginal()
    {
        Assert.Equal("Hola", _helper.Truncate("Hola", 4));
    }

    [Fact]
    public void Truncate_TextLongerThanLimit_ReturnsTruncatedWithSuffix()
    {
        Assert.Equal("Hola...", _helper.Truncate("Hola Mundo", 4));
    }

    [Fact]
    public void Truncate_CustomSuffix_UsesCustomSuffix()
    {
        Assert.Equal("Hola!", _helper.Truncate("Hola Mundo", 4, "!"));
    }

    [Fact]
    public void Truncate_ZeroMaxLength_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _helper.Truncate("Hola", 0));
    }

    [Fact]
    public void Truncate_NegativeMaxLength_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _helper.Truncate("Hola", -5));
    }

    // --- toSlug ---

    [Fact]
    public void ToSlug_SimpleText_ReturnsSlug()
    {
        Assert.Equal("hola-mundo", _helper.ToSlug("Hola Mundo"));
    }

    [Fact]
    public void ToSlug_TextWithSpecialChars_RemovesThem()
    {
        Assert.Equal("hola-mundo-2024", _helper.ToSlug("¡Hola Mundo! 2024"));
    }

    [Fact]
    public void ToSlug_EmptyText_ReturnsEmpty()
    {
        Assert.Equal(string.Empty, _helper.ToSlug(""));
    }

    [Fact]
    public void ToSlug_MultipleSpaces_CollapseToSingleDash()
    {
        Assert.Equal("hola-mundo", _helper.ToSlug("hola   mundo"));
    }

    // --- countWords ---

    [Fact]
    public void CountWords_NormalText_ReturnsCorrectCount()
    {
        Assert.Equal(2, _helper.CountWords("Hola Mundo"));
    }

    [Fact]
    public void CountWords_MultipleSpaces_CountsCorrectly()
    {
        Assert.Equal(3, _helper.CountWords("Hola   Mundo   2024"));
    }

    [Fact]
    public void CountWords_EmptyString_ReturnsZero()
    {
        Assert.Equal(0, _helper.CountWords(""));
    }

    [Fact]
    public void CountWords_OnlySpaces_ReturnsZero()
    {
        Assert.Equal(0, _helper.CountWords("     "));
    }
}

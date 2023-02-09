using Battleships.Console.Exceptions;
using FluentAssertions;

namespace Battleships.Console.Tests;

internal class InputTranslatorTests
{
    [Test]
    [TestCase("a0", 0, 0)]
    [TestCase("b5", 1, 5)]
    [TestCase("j9", 9, 9)]
    public void GetCoordinatesFromInput_ValidInput_ReturnsParsedCoordinates(string input, int x, int y)
    {
        var translator = new InputTranslator();
        var (resX, resY) = translator.GetCoordinatesFromInput(input);
        resX.Should().Be(x);
        resY.Should().Be(y);
    }

    [Test]
    [TestCase("")]
    [TestCase("asdf")]
    [TestCase("AB11")]
    [TestCase("A11")]
    [TestCase("AB1")]
    [TestCase("Z1")]
    public void GetCoordinatesFromInput_InvalidInput_ThrowsInvalidInputException(string input)
    {
        var translator = new InputTranslator();
        Assert.Throws<InvalidInputException>(() => translator.GetCoordinatesFromInput(input));
    }
}
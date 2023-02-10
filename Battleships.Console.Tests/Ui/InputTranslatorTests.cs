using Battleships.Console.Ui;
using FluentAssertions;

namespace Battleships.Console.Tests.Ui;

internal class InputTranslatorTests
{
    [Test]
    [TestCase("a0", 0, 0)]
    [TestCase("b5", 1, 5)]
    [TestCase("j9", 9, 9)]
    public void GetCoordinatesFromInput_ValidInput_ReturnsParsedCoordinates(string input, int x, int y)
    {
        var translator = new InputTranslator();
        translator.TryGetCoordinatesFromInput(input, out var coordinates);
        coordinates.X.Should().Be(x);
        coordinates.Y.Should().Be(y);
    }

    [Test]
    [TestCase("")]
    [TestCase("asdf")]
    [TestCase("AB11")]
    [TestCase("A11")]
    [TestCase("AB1")]
    [TestCase("Z1")]
    public void GetCoordinatesFromInput_InvalidInput_ReturnsFalse(string input)
    {
        var translator = new InputTranslator();
        var result = translator.TryGetCoordinatesFromInput(input, out _);
        result.Should().BeFalse();
    }
}
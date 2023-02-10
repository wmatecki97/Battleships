using Battleships.Console.Models;
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
        //Arrange
        var translator = new InputTranslator();
        var expectedResult = new Coordinates
        {
            X = x,
            Y = y
        };

        //Act
        translator.TryGetCoordinatesFromInput(input, out var coordinates);

        //Assert
        coordinates.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    [TestCase("")]
    [TestCase("asdf")]
    [TestCase("AB11")]
    [TestCase("A11")]
    [TestCase("AB1")]
    [TestCase("Z1")]
    [TestCase("1A")]
    public void GetCoordinatesFromInput_InvalidInput_ReturnsFalse(string input)
    {
        //Arrange
        var translator = new InputTranslator();

        //Act
        var result = translator.TryGetCoordinatesFromInput(input, out _);

        //Assert
        result.Should().BeFalse();
    }
}
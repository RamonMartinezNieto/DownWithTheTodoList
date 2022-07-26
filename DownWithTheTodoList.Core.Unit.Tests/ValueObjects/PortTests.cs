namespace DownWithTheTodoList.Core.Unit.Tests.ValueObjects;

public class PortTests
{

    [Theory]
    [InlineData(0)]
    [InlineData(65535)]
    [InlineData(23412)]
    public void Should_NotThrow_Any_Exception(int value)
    {
        new Port(value);
    }

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_When_Port_IsNegative()
    {
        Action action = () => new Port(-1);

        action.Should()
            .Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_Be_Same_Value_As_Constructor()
    {
        Port port = new(3306);

        port.Value.Should().Be(3306);

    }
}
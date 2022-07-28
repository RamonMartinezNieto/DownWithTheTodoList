using NSubstitute;

namespace DownWithTheTodoList.Core.Unit.Tests;

public class EnsureTests
{
    [Theory]
    [InlineData(65,67)]
    [InlineData(66,66)]
    public void Should_Not_Throw_Any_Exception_When_Is_In_Range(int start, int end) 
    {
        Ensure.That(66).IsBetween(start, end);
    }

    [Theory]
    [InlineData(25, 65, 67)]
    [InlineData(101, 66, 66)]
    public void Should_Throw_ArgumentOutOfRangeException_When_Is_OutOfRange(int sut, int start, int end) 
    {

        Action action = () => Ensure.That(sut).IsBetween(start, end);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_When_Is_Less_Than_Cero()
    {
        Action action = () => Ensure.That(-345).IsCeroOrPositive();

        action.Should().Throw<ArgumentOutOfRangeException>();
    }    
    
    [Fact]
    public void Should_Not_Throw_Any_Exception_When_Is_Greater_Than_Cero()
    {
        Ensure.That(345).IsCeroOrPositive();
    }   

    [Fact]
    public void Should_Not_Throw_Any_Exception_When_Is_Greater_Than_The_Number()
    {
        Ensure.That(345).IsGreaterThan(123);
    }


    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_When_Is_Less_Than_The_Number()
    {
        Action action = () => Ensure.That(66).IsGreaterThan(77);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_Not_Throw_Any_Exception_When_Is_Less_Than_The_Number()
    {
        Ensure.That(123).IsLessThan(444);
    }

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_When_Is_Greater_Than_The_Number()
    {
        Action action = () => Ensure.That(66).IsLessThan(55);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_Not_Throw_Any_Exception_When_Is_Equals_To_The_Number()
    {
        Ensure.That(123).IsEquals(123);
    }

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_When_Is_Not_Equals_To_The_Number()
    {
        Action action = () => Ensure.That(66).IsLessThan(55);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_Not_Throw_Any_Exception_When_Is_Not_Null_Or_Empty() 
    {
        Ensure.That("wiiii").NotNullOrEmpty();
    }

    [Fact]
    public void Should_Throw_Exception_WhenString_Is_Null()
    {
        Action action = () => Ensure.That<string>(null!).NotNullOrEmpty();

        action.Should().Throw<Exception>();
    }

    [Fact]
    public void Should_Throw_Exception_WhenString_Is_Emprty()
    {
        Action action = () => Ensure.That("").NotNullOrEmpty();

        action.Should().Throw<Exception>();
    }

    [Theory]
    [InlineData("1.1.1.1")]
    [InlineData("22.22.22")]
    [InlineData("333.333")]
    public void Should_Not_Throw_Exception_When_Format_Is_Correct(string value) 
    {
        var pattern = "^(\\d+\\.)?(\\d+\\.)?(\\d+\\.)?(\\*|\\d+)$";
        Ensure.That(value).HaveFormat(pattern);
    }

    [Fact]
    public void Should_Throw_Exception_When_Format_Is_Wrong() 
    {
        var pattern = "^(\\b+\\.)?(\\b+\\.)?(\\b+\\.)?(\\*|\\d+)$";

        Action action = () => Ensure.That("1.1.1.1").HaveFormat(pattern);

        action.Should().Throw<FormatException>();
    }


    [Fact]
    public void Should_Throw_Exception_WhenSomeObject_IsNull()
    {
        Object? obj = null;
        Action action = () => Ensure.That(obj).IsNotNull();

        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ShouldThrow_CustomException_WhenSomeObjectIsNull_AndExceptionIsDefined()
    {
        Object? obj = null;
        Action action = () => Ensure.That(obj).IsNotNull<KeyNotFoundException>();

        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void ShouldThrow_CustomException_WhenSomeObjectIsNull_ExceptionIsDefined_WithCustomMessage()
    {
        Object? obj = null;
        Action action = () => Ensure.That(obj).IsNotNull<KeyNotFoundException>("Null exception man!");

        action.Should()
            .Throw<KeyNotFoundException>()
            .WithMessage("Null exception man!");
    }


    [Fact]
    public void ShouldNotThrowException_WhenSomeObject_IsNotNull()
    {
        Object? obj = new ();
        Action action = () => Ensure.That(obj).IsNotNull();
    }

    [Fact]
    public void ShouldNotThrowException_WhenSomeObject_IsNotNull_CustomException()
    {
        Object? obj = new ();
        Action action = () => Ensure.That(obj).IsNotNull<KeyNotFoundException>();
    }
    [Fact]
    public void ShouldNotThrowException_WhenSomeObject_IsNotNull_CustomException_And_Custom_Message()
    {
        Object? obj = new ();
        Action action = () => Ensure.That(obj).IsNotNull<KeyNotFoundException>("Null exception man!");
    }

}
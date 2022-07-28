namespace DownWithTheTodoList.Core.ValueObjects;

public class Port
{
    private readonly int _value;

    public Port(int value)
    {
        Ensure.That(value).IsBetween(0, 65535);

        _value = value;
    }

    public override string ToString()
        => _value.ToString();

    public static implicit operator int(Port _port) 
        => _port._value;

    public static explicit operator Port(int _port) 
        => new (_port);

    protected IEnumerable<object> GetAtomicValues() 
    {
        yield return _value;
    }
}

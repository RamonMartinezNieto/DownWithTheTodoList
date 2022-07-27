namespace DownWithTheTodoList.Core.ValueObjects;

public class Port
{
    public int Value { get; }

    public Port(int value)
    {
        Ensure.That(value).IsBetween(0, 65535);

        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator int(Port _port) 
        => _port.Value;

    public static explicit operator Port(int _port) 
        => new Port(_port);
}

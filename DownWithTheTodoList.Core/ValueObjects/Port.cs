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
}

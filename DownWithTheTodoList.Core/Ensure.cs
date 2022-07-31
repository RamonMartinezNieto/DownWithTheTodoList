namespace DownWithTheTodoList.Core;

public static class Ensure
{
    private static void That<TException>(bool condition, string message) where TException : notnull, Exception
    {
        if (!condition)
        {
            throw (TException)Activator.CreateInstance(typeof(TException), message)!;
        }
    }
        
    private static void That<TException>(bool condition, int param) where TException : Exception
    {
        if (!condition)
        {
            throw (TException)Activator.CreateInstance(typeof(TException), param.ToString())!;
        }
    }

    public static T That<T>(T value) => value;

    public static void IsBetween(this int value, int init, int end)
    {
        That<ArgumentOutOfRangeException>(init <= value && end >= value, value);
    }

    public static void NotNullOrEmpty(this string value) 
    {
        That<Exception>(!String.IsNullOrEmpty(value), "String cannot be null or empty");
    }

    public static void IsNotNull(this object? value)
    {
        That<ArgumentNullException>(value is not null, "Object cannot be null or empty");
    }

    public static void IsNotNull<T>(this object? value, string message) where T : Exception
    {
        That<T>(value is not null, message);
    }
        
    public static void IsNotNull<T>(this object? value) where T : Exception
    {
        That<T>(value is not null, "Object cannot be null or empty");
    }

    public static void HaveFormat(this string value, string pattern) 
    {
        That<FormatException>(Regex.IsMatch(value, pattern), $"Invalid format of value {value}, check regex pattern");
    }

    public static void IsCeroOrPositive(this int value) 
    {
        That<ArgumentOutOfRangeException>(value >= 0, "Value can't be less than 0");
    }

    public static void IsGreaterThan(this int value, int compare) 
    {
        That<ArgumentOutOfRangeException>(value > compare, $"Value can't be less than {compare}");
    }    
    
    public static void IsLessThan(this int value, int compare) 
    {
        That<ArgumentOutOfRangeException>(value < compare, $"Value can't be less than {compare}");
    }

    public static void IsEquals(this int value, int equality) 
    {
        That<ArgumentOutOfRangeException>(value == equality, $"Value can't be different than {equality}");
    }

    public static void IsTrue<TException>(this bool value, string message) where TException :  Exception
    {
        That<TException>(value, message);
    }

    public static void IsNotDefault<TObject, TException>(this TObject value, string message) where TException : Exception
    {
        That<TException>(!value!.Equals(default(TObject)), message);
    }

}

namespace EchoOutLogging;
public delegate void EchoOutputLogger(string? output);
public delegate string ConcatMathOp(EchoOut? lhs, EchoOut rhs, dynamic? c, int counter, string opSign);
public delegate string LhsConcatTitle(EchoOutTitle lhs, EchoOut rhs);
public delegate string RhsConcatTitle(EchoOut lhs, EchoOutTitle rhs);
public delegate string ValueToOutput(dynamic? val);

public class EchoOutFactory
{
    public static EchoOutputLogger OutputLogger { get; set; } = DefaultOutputLogger;
    public static ConcatMathOp ConcatMathOp { get; set; } = DefaultConcatMathOp;
    public static LhsConcatTitle LhsConcatTitle { get; set; } = DefaultLhsConcatTitle;
    
    public static RhsConcatTitle RhsConcatTitle { get; set; } = DefaultRhsConcatTitle;

    public static ValueToOutput ValueToOutput { get; set; } = DefaultValueToOutput;


    
    private static string DefaultRhsConcatTitle(EchoOut lhs, EchoOutTitle rhs)
    {
        return $"{lhs.Output}{rhs.Output}";
    }
    
    private static void DefaultOutputLogger(string? output)
    {
        Console.WriteLine(output);
    }
    
    private static string DefaultConcatMathOp(EchoOut lhs, EchoOut? rhs, dynamic? c, int counter, string opSign)
    {
        return $"{lhs?.Output}{counter}[{lhs?.Val}{opSign}{rhs?.Val} = {c}] {rhs?.Output}";
    }
    
    private static string DefaultLhsConcatTitle(EchoOutTitle lhs, EchoOut rhs)
    {
        return $"{lhs.Output}{rhs.Output}";
    }

    private static string DefaultValueToOutput(dynamic? val)
    {
        return $"{val}";
    }
    
}
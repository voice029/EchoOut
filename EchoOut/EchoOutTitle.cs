namespace EchoOutLogging;

public class EchoOutTitle
{
    public string Output;
    public LhsConcatTitle LhsConcatTitle;
    public RhsConcatTitle RhsConcatTitle;
    public EchoOutTitle(string output)
    {
        LhsConcatTitle = EchoOutFactory.LhsConcatTitle;
        RhsConcatTitle = EchoOutFactory.RhsConcatTitle;
        this.Output = output;
    }
    public static implicit operator EchoOutTitle(string title)
    {
        return new EchoOutTitle(title);
    }
        
    public static dynamic operator +(EchoOutTitle outTitle, dynamic rhs)
    {
        EchoOut o = EchoOutExt.echo(rhs);
        o.Output = outTitle.Output;
        var real = o.trueSelf();
        return real;
    }
        
    public static EchoOut operator +(EchoOutTitle outTitle, EchoOut echoOut)
    {
        echoOut.Output = outTitle.LhsConcatTitle(outTitle ,echoOut) ;
        return echoOut;
    }
        
    public static dynamic operator |(EchoOutTitle outTitle, EchoOut rhs)
    {
        rhs.Output = outTitle.LhsConcatTitle(outTitle, rhs);
        var real = rhs.trueSelf();
        return real;
    }
    
    public static dynamic operator |(EchoOutTitle outTitle, dynamic rhs)
    {
        EchoOut o = EchoOutExt.echo(rhs);
        o.Output = outTitle.Output + rhs;
        var real = o.trueSelf();
        return real;
    }
        
}
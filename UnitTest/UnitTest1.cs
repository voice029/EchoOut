using System.Diagnostics;
using System.Reflection;
using VoiceOut;
using VoiceOut.EchoOutExtStarter;
using Xunit.Sdk;

namespace UnitTest2;
using Xunit;

// [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class UnitTest1 
{
    public string logOut = "";

    public UnitTest1()
    {
        init();
    }
    void init()
    {
        EchoOutFactory.OutputLogger = OutputLogger;
    }

    private void OutputLogger(string output)
    {
        logOut = output;
    }
    
    [Fact]
    public void Print_Add()
    {
        int val = 2.echo() + 3;
        Assert.StrictEqual("1[2+3 = 5] ", logOut); 
        
        val = 2.echo() + 3 + 4;
        Assert.StrictEqual("1[2+3 = 5] 2[5+4 = 9] ", logOut); 
    }
    
    [Fact]
    public void Print_Sub()
    {
        int val = 3.echo() - 2;
        Assert.StrictEqual("1[3-2 = 1] ", logOut); 
        
        val = 10.echo() - 5 - 4;
        Assert.StrictEqual("1[10-5 = 5] 2[5-4 = 1] ", logOut); 
    }

    [Fact]
    public void Print_Mul()
    {
        int val = 3.echo() * 2;
        Assert.StrictEqual("1[3*2 = 6] ", logOut); 
        
        val = 10.echo() * 5 * 4;
        Assert.StrictEqual("1[10*5 = 50] 2[50*4 = 200] ", logOut); 
    }
    
    [Fact]
    public void Print_Div()
    {
        int val = 6.echo() / 3;
        Assert.StrictEqual("1[6/3 = 2] ", logOut); 
        
        val = 200.echo() / 4 / 10;
        Assert.StrictEqual("1[200/4 = 50] 2[50/10 = 5] ", logOut); 
    }
    
}
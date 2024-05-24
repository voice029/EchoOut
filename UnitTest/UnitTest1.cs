using System.Diagnostics;
using System.Reflection;
using VoiceOut;
using VoiceOut.EchoOutExtStarter;
using Xunit.Sdk;

namespace UnitTest2;
using Xunit;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class UnitTest1 : BeforeAfterTestAttribute
{
    void init()
    {
        EchoOutFactory.OutputLogger = OutputLogger;

    }
    
    public override void Before(MethodInfo methodUnderTest)
    {
        
    }

    private void OutputLogger(string output)
    {
        logOut = output;
    }

    public string logOut = "";
    
    [Fact]
    public void Print_Add()
    {
        init();
        
        int val = 2.echo() + 3;
        Assert.StrictEqual("1[2+3 = 5] ", logOut); 
        
        val = 2.echo() + 3 + 4;
        Assert.StrictEqual("1[2+3 = 5] 2[5+4 = 9] ", logOut); 
    }
    
    [Fact]
    public void Print_Sub()
    {
        init();
        
        int val = 3.echo() - 2;
        Assert.StrictEqual("1[3-2 = 1] ", logOut); 
        
        val = 10.echo() - 5 - 4;
        Assert.StrictEqual("1[10-5 = 5] 2[5-4 = 1] ", logOut); 
    }
}
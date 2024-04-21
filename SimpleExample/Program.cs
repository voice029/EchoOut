// See https://aka.ms/new-console-template for more information

using VoiceOut.EchoOutClasses;
using VoiceOut.EchoOutExtStarter;
//
// int someValue = 5;
// int someV2 = 100;
//
// // bool test = "echo ".echo() + ("-- ".echo() + 1 * 10.echo() + someV2).Equals(110);
// // bool test = 30 < 20.echo();
// // Console.WriteLine(test);
// int test2 = 20;
// // int test3 = -test2.echo() + 20;
//
//
// test2.echo("test 2 ").IfEq(20).True().Log(false).echo("more strings").Log();

// Factory.Value2 = 3;
//
// var test = Factory.Inst;
//     
// public class FactoryInst
// {
//     protected static FactoryInst Instance;
//     protected static FactoryEntry<FactoryInst> Entry = new();
//     static FactoryInst()
//     {
//         var isNull = Entry;
//         Console.WriteLine("Factory Inst");
//         
//     }
//
//     protected static int InitMethod<T>() => 2;
//     public static int Value = 1;
// }
//
// public class FactoryEntry<T> : FactoryInst where T : class, new()
// {
//     public static Lazy<T> Instance;
//     
//     static FactoryEntry()
//     {
//         T empty = new T();
//         FactoryInst.Instance = new FactoryEntry<T>();
//         Console.WriteLine("Factory Entry");
//
//         // Instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T)));
//     }
// }
//
// public class Custom : FactoryEntry<Custom>
// {
//     private int Trigger = InitMethod<Custom>();
//     static Custom()
//     {
//         Console.WriteLine("static custom constructor called");
//     }
//
//     public Custom()
//     {
//         Console.WriteLine("Custom constructor called");
//     }
//     
//     private static FactoryEntry<Custom> addMe = new();
// }
//
// public class Factory : FactoryInst
// {
//     public static FactoryInst Inst
//     {
//         get => FactoryInst.Instance;
//     }
//     
//     public static int Value2 = 2;
//     static Factory()
//     {
//         Console.WriteLine("Factory");
//     }
//     public int Test()
//     {
//         return 1;
//     }
// }

int val = 2;
int log = val.echo();
    


public class EchoCustom<T> : EchoOut<T>
{
    public EchoCustom() {}

    public EchoCustom(T aVal) : base(aVal) { }

    public static implicit operator EchoCustom<T>(T val)
    {
        return new EchoCustom<T>(val);
    }
}



public interface IFactory
{
    public delegate int TestFunc();
    TestFunc GetTestFunc();
}

public class FactoryBase
{
    private static IFactory instance;
    public static IFactory.TestFunc GetFunc()
    {
        return instance.GetTestFunc();
    }
}

public class FactoryBase<T>
{
    
}

public class Factory : FactoryBase
{
    private IFactory.TestFunc GetValue = GetFunc();
    
}

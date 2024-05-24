
using System;
using VoiceOut.EchoOutClasses;

namespace VoiceOut
{
    public delegate void EchoOutputLogger(string output);

    public delegate string ConcatMathOp(EchoOut lhs, EchoOut rhs, dynamic c, int counter, string opSign, PrintOutOrder printOrder);

    public delegate string LhsConcatTitle(EchoOutTitle lhs, EchoOut rhs);

    public delegate string RhsConcatTitle(EchoOut lhs, EchoOutTitle rhs);

    public delegate string ValueToOutput(dynamic val);
    
    public class EchoOutFactory
    {
        // TODO more work on how to setup the factory
        public static DefaultSystems DefaultCalls = new DefaultSystems();

        public static void SetCalls<T>(T calls) where T : DefaultSystems
        {
            DefaultCalls = calls;
        }
        
        public static EchoOutputLogger OutputLogger { get; set; } =
            DefaultCalls?.DefaultOutputLogger() ?? DefaultOutputLogger;

        public static ConcatMathOp ConcatMathOp { get; set; } =
            DefaultCalls?.DefaultConcatMathOp() ?? DefaultConcatMathOp;

        public static LhsConcatTitle LhsConcatTitle { get; set; } =
            DefaultCalls?.DefaultLhsConcatTitle() ?? DefaultLhsConcatTitle;

        public static RhsConcatTitle RhsConcatTitle { get; set; } =
            DefaultCalls?.DefaultRhsConcatTitle() ?? DefaultRhsConcatTitle;

        public static ValueToOutput ValueToOutput { get; set; } =
            DefaultCalls?.DefaultValueToOutput() ?? DefaultValueToOutput;
        



        private static string DefaultRhsConcatTitle(EchoOut lhs, EchoOutTitle rhs)
        {
            return $"{lhs.Output}{rhs.Output}";
        }

        private static void DefaultOutputLogger(string output)
        {
            Console.WriteLine(output);
        }

        private static string DefaultConcatMathOp(EchoOut lhs, EchoOut rhs, dynamic c, int counter, string opSign, PrintOutOrder printOutOrder)
        {
            switch (printOutOrder)
            {
                case PrintOutOrder.MatchTerms:
                    return $"{lhs?.Output}{counter}[{lhs?.Val}{opSign}{rhs?.Val} = {c}] {rhs?.Output}";
                case PrintOutOrder.Sequential:
                    return $"{lhs?.Output} {rhs?.Output}{counter}[{lhs?.Val}{opSign}{rhs?.Val} = {c}] ";
                default: 
                    return $"{lhs?.Output} {rhs?.Output}{counter}[{lhs?.Val}{opSign}{rhs?.Val} = {c}] ";
            }
        }

        private static string DefaultLhsConcatTitle(EchoOutTitle lhs, EchoOut rhs)
        {
            return $"{lhs.Output}{rhs.Output}";
        }

        private static string DefaultValueToOutput(dynamic val)
        {
            return $"{val}";
        }

    }

    public class DefaultSystems
    {
        public virtual RhsConcatTitle DefaultRhsConcatTitle()
        {
            return aDefaultRhsConcatTitle;
        }

        public virtual EchoOutputLogger DefaultOutputLogger()
        {
            return aDefaultOutputLogger;
        }

        public virtual ConcatMathOp DefaultConcatMathOp()
        {
            return aDefaultConcatMathOp;
        }

        public virtual LhsConcatTitle DefaultLhsConcatTitle()
        {
            return aDefaultLhsConcatTitle;
        }

        public virtual ValueToOutput DefaultValueToOutput()
        {
            return aDefaultValueToOutput;
        }

        private string aDefaultRhsConcatTitle(EchoOut lhs, EchoOutTitle rhs)
        {

            return $"{lhs.Output}{rhs.Output}";
        }

        private void aDefaultOutputLogger(string output)
        {
            Console.WriteLine(output);
        }

        private string aDefaultConcatMathOp(EchoOut lhs, EchoOut rhs, object c, int counter, string opSign, PrintOutOrder printOrder)
        {
            switch (printOrder)
            {
                case PrintOutOrder.MatchTerms:
                    return $"{lhs?.Output}{counter}[{lhs?.Val}{opSign}{rhs?.Val} = {c}] {rhs?.Output}";
                default: // PrintOutOrder.Sequential
                    return $"{lhs?.Output}{rhs?.Output}{counter}[{lhs?.Val}{opSign}{rhs?.Val} = {c}] ";
            }
        }

        private string aDefaultLhsConcatTitle(EchoOutTitle lhs, EchoOut rhs)
        {
            return $"{lhs.Output}{rhs.Output}";
        }

        private string aDefaultValueToOutput(dynamic val)
        {
            return $"{val}";
        }
        
    }
}
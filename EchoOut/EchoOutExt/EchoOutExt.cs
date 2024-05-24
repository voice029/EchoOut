using System;
using VoiceOut.EchoOutClasses;

namespace VoiceOut.EchoOutExt
{
    
    public static class EchoOutExt
    {
        public static EchoOut<T> echo<T>(this T obj, object toString)
        {
            return new EchoOut<T>(obj)
            {
                // Val = obj,
                Output = toString?.ToString() ?? "",
            };
        }
        
        public static EchoOut<T> echo<T>(EchoOut<T> obj)
        {
            return obj;
        }

        public static EchoOut<bool> echoIf(this bool obj, object toStringIfTrue, object toStringIfFalse)
        {
            if (obj)
            {
                obj.echo(toStringIfTrue?.ToString());
            }
            else
            {
                obj.echo(toStringIfFalse?.ToString());
            }

            return obj;
        }
        
        
        public static EchoOut<T> Type<T>(this EchoOut<T> builder)
        {
            builder.Output += typeof(T) + " ";
            return builder;
        }

        public static EchoOut<T> Get<T>(this EchoOut<T> builder, Func<T, string> getStr)
        {
            builder.Output += getStr(builder.Val);
            return builder;
        }

        public static EchoOut<T> Eq<T, T2>(this EchoOut<T> builder, T2 target, string ifTrue)
        {
            if (builder.Val != null && builder.Val == target)
            {
                // builder.lastSuccCompare = 0;
                builder.Output += ifTrue;
            }

            return builder;
        }

        public static EchoOut<T> Less<T, T2>(this EchoOut<T> builder, T2 target, string ifTrue)
            where T : IComparable<T2>
        {
            if (builder.Val?.CompareTo(target) < 0)
            {
                builder.Output += ifTrue;
            }
            
            return builder;
        }

        public static EchoOut<T> Greater<T, T2>(this EchoOut<T> builder, T2 target, string ifTrue)
            where T : IComparable<T2>
        {
            if (builder.Val?.CompareTo(target) > 0)
            {
                builder.Output += ifTrue;
            }

            return builder;
        }

        public static EchoOut<T> IfTrue<T>(this EchoOut<T> builder, string ifTrue)
        {
            if (builder.Val)
            {
                builder.Output += ifTrue;
            }

            return builder;
        }

        public static EchoOut<T> Concat<T>(this EchoOut<T> builder, string concat)
        {
            builder.Output += concat;
            return builder;
        }

        public static EchoOut<T> Format<T>(this EchoOut<T> builder, string format)
        {
            builder.Output += String.Format(format, builder.Val);
            return builder;
        }

        public static EchoOut<T> Log<T>(this EchoOut<T> builder, bool flush = true)
        {
            if (builder.conditionalPrintState.HasValue)
            {
                if (builder.conditionalPrintVal.HasValue && builder.conditionalPrintVal != builder.conditionalPrintState)
                {
                    return builder;
                }
            }
            Log(builder.Output);
            if (flush)
            {
                builder.Output = "";
            }
            return builder;
        }

        public static void Log(object obj)
        {
            Console.WriteLine(obj?.ToString());
        }
    }
}
using EchoOutLogging;

namespace EchoOutLogging
{
    public static class EchoOutExt
    {
        public static EchoOutTitle echo(this string obj)
        {
            return new EchoOutTitle(obj);
        }
        
        public static EchoOutTitle echoLog(this string obj)
        {
            return new EchoOutTitle(obj){ OutputLogger = EchoOutFactory.OutputLogger};
        }

        public static EchoOut<T> echo<T>(this T obj)
        {
            return new EchoOut<T>()
            {
                Val = obj,
                Output = "",
            };
        }
        
        public static EchoOut<T> echo<T>(this T obj, object? toString)
        {
            return new EchoOut<T>()
            {
                Val = obj,
                Output = toString?.ToString() ?? "",
            };
        }
        
        public static EchoOut<int> echo(this int obj)
        {
            return new EchoOut<int>()
            {
                Val = obj,
                Output = "",
            };
        }
        
        public static EchoOut<T> echo<T>(EchoOut<T> obj)
        {
            return obj;
        }

        public static EchoOut<bool> echoIf(this bool obj, object? toStringIfTrue, object? toStringIfFalse)
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
        
        
        public static EchoOut BriefTitle<T>(this T obj, string id)
        {
            return new EchoOut()
            {
                Val = obj,
                Output = id,
                Id = id
            };
        }
        
        public static EchoOut Type(this EchoOut builder)
        {
            return builder;
        }

        public static EchoOut Get<T>(this EchoOut builder, Func<T, string> getStr)
        {
            builder.Output += getStr(builder.Val);
            return builder;
        }

        public static EchoOut Eq<T, T2>(this EchoOut builder, T2 target, string ifTrue)
        {
            if (builder.Val != null && builder.Val?.Equals(target) == true)
            {
                // builder.lastSuccCompare = 0;
                builder.Output += ifTrue;
            }

            return builder;
        }

        public static EchoOut Less<T, T2>(this EchoOut builder, T2 target, string ifTrue)
            where T : IComparable<T2>
        {
            if (builder.Val?.CompareTo(target) < 0)
            {
                // builder.lastSuccCompare = -1;
                builder.Output += ifTrue;
            }

            return builder;
        }

        public static EchoOut Greater<T, T2>(this EchoOut builder, T2 target, string ifTrue)
            where T : IComparable<T2>
        {
            if (builder.Val?.CompareTo(target) > 0)
            {
                builder.Output += ifTrue;
            }

            return builder;
        }

        public static EchoOut IfTrue(this EchoOut builder, string ifTrue)
        {
            if (builder.Val)
            {
                builder.Output += ifTrue;
            }

            return builder;
        }

        public static EchoOut Concat(this EchoOut builder, string concat)
        {
            builder.Output += concat;
            return builder;
        }

        public static EchoOut Format(this EchoOut builder, string format)
        {
            builder.Output += String.Format(format, builder.Val);
            return builder;
        }

        public static EchoOut Log(this EchoOut builder, bool flush = true)
        {
            if (builder.conditionalState.HasValue)
            {
                if (builder.lastConditional.HasValue && builder.lastConditional != builder.conditionalState)
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

        public static void Log(object? obj)
        {
            Console.WriteLine(obj?.ToString());
        }
    }
}

using VoiceOut.EchoOutClasses;

namespace VoiceOut.EchoOutExtStarter
{
    public static class EchoOutExtStarter
    {
        public static EchoOut<T> echo<T>(this T obj)
        {
            return new EchoOut<T>(obj)
            {
                Output = "",
            };
        }
        
        public static EchoOut<T> echo<T>(this T obj, object toString)
        {
            return new EchoOut<T>(obj)
            {
                Output = toString?.ToString() ?? "",
            };
        }
        
        public static EchoOut<T> echo<T>(EchoOut<T> obj)
        {
            return obj;
        }

    }
}


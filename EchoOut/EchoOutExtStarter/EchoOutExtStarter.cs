
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

    }
}


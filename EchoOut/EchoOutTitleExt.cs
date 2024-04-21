namespace VoiceOut
{
    public static class EchoOutTitleExt
    {
        public static EchoOutTitle echo(this string obj)
        {
            return new EchoOutTitle(obj);
        }
        
        public static EchoOutTitle echoLog(this string obj)
        {
            return new EchoOutTitle(obj){ OutputLogger = EchoOutFactory.OutputLogger};
        }
    }
}
namespace VoiceOut.EchoOutClasses
{
    public interface IEchoOutIfOptions<T>
    {
        IEchoOutBranchOptions<T> True();
        IEchoOutBranchOptions<T> False();
    }
}
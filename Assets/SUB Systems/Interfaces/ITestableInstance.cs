namespace SUBS.Core.Testing
{
    public interface ITestableInstance
    {
#if UNITY_EDITOR
        bool TestingInstance { get; }
#endif
    }
}

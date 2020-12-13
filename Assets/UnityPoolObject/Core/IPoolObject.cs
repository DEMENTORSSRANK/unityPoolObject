namespace UnityPoolObject.Core
{
    public interface IPoolObject<out T>
    {
        T Group { get; }

        void Create();

        void OnPush();

        void FailedPush();
    }
}
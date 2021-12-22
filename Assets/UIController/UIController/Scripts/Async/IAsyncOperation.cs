namespace Elka.UI.Controller
{
    public interface IAsyncOperation<T>
    {
        bool IsValid { get; }
        bool IsDone { get; }
        T Result { get; }
    }
}
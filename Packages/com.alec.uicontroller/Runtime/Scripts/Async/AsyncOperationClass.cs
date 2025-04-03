namespace Elka.UI.Controller
{
#if !async
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class AsyncOperationClass<T> : IAsyncOperation<T>
{

    AsyncOperationHandle<T> asyncOperation;

    public AsyncOperationClass(AsyncOperationHandle<T> asyncOperation)
    {

        this.asyncOperation = asyncOperation; 

    }
    public bool IsDone => asyncOperation.IsDone;

    public T Result => asyncOperation.Result;

    public bool IsValid =>  asyncOperation.IsValid();

}
#endif
}
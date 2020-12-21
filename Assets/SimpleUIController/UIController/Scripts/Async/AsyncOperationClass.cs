using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if async
using UnityEngine.ResourceManagement.AsyncOperations;

public class AsyncOperationClass<T> : IAsyncOperation<T>
{

    AsyncOperationHandle<T> asyncOperation;

    public AsyncOperationClass(AsyncOperationHandle<T> asyncOperation)
    {

        this.asyncOperation = asyncOperation; 

    }

    public bool IsDone {get { if (asyncOperation == null) return false; else return asyncOperation.IsDone; } }

    public T Result {get { if (asyncOperation == null) return default(T); else return asyncOperation.Result; } }

    public bool IsValid { get { if (asyncOperation == null) return false; return asyncOperation.IsValid(); } }

}
#endif

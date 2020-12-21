using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssetManager<T> 
{
#if async
    IAsyncOperation<T> InstantiateAsync(string assetName);
    bool ReleaseInstance(T t);
#else
    T Instantiate(T t);
    void ReleaseInstance(T t);
#endif
}

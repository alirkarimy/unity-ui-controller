using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager :MonoBehaviour, IAssetManager<GameObject>
{
    public static AssetManager Instance = null;

    private void Awake()
    {
        if (Instance)
            ReleaseInstance(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
#if async
    public IAsyncOperation<GameObject> InstantiateAsync(string assetName)
    {

        new AsyncOperationClass<GameObject>(Addressables.InstantiateAsync(assetName)); 
    }
     public bool ReleaseInstance(GameObject t)
    {
        if(t)
            Addressables.ReleaseInstance(GetInstantiatable());
    }
#else
    public GameObject Instantiate(GameObject t)
    {
        return Instantiate(t);
    }

    public void ReleaseInstance(GameObject t)
    {
       Destroy(t);
    }
#endif




}

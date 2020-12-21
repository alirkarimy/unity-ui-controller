using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Factory design pattern with generic twist!
/// </summary>
public class UIFactory : MonoBehaviour, IUIFactory
{
    // Reference to prefab of IUserInterfaces. handled by FactoryInspector custom Editor
    [HideInInspector]
    public UserInterface[] baseDialogs;

    /// <summary>
    /// Creating new instance of prefab.
    /// </summary>
    /// <returns>New instance of prefab.</returns>
    public IUserInterface GetUI(UIType type)
    {
        IUserInterface dialog = baseDialogs[(int)type];

        if (dialog == null) return null;
        if (dialog.GetInstantiatable() == null) return null;

        return Instantiate(dialog.GetInstantiatable(), transform.position, transform.rotation).GetComponent<IUserInterface>();
    }

#if async
    public IAsyncOperation<GameObject> GetUIAsync(UIType type)
    {
        return AssetManager.Instance.InstantiateAsync(type.ToString());
    }
#endif


}


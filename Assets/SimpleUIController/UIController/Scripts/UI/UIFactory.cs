using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIController
{
    /// <summary>
    /// Factory design pattern with generic twist!
    /// </summary>
    public class UIFactory : MonoBehaviour, IUIFactory
    {

        /// <summary>
        /// Creating new instance of prefab.
        /// </summary>
        /// <returns>New instance of prefab.</returns>
        public IUserInterface GetUI(UIType type)
        {
            return AssetManager.Instantiate(type);
        }

        /// <summary>
        /// Creating new async instance of prefab.
        /// </summary>
        /// <returns>New instance of prefab.</returns>
        public IAsyncOperation<GameObject> GetUIAsync(UIType type)
        {
            return AssetManager.InstantiateAsync(type.ToString());
        }


    }

}
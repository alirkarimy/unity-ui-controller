using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elka.UI.Controller
{
    public class UIFactory : MonoBehaviour, IUIFactory
    {
        private readonly Dictionary<string, IUserInterface> _cache =
            new Dictionary<string, IUserInterface>(StringComparer.OrdinalIgnoreCase);

        public IUserInterface GetUIAsync(string pageName)
        {
            // 1) Cached?
            if (_cache.TryGetValue(pageName, out var cached))
            {
                if (cached != null)
                {
                    cached.PageName = pageName;
                    return cached;
                }
                _cache.Remove(pageName); // cleanup dead reference
            }

            // 2) Instantiate via Addressables
            var go = AssetManager.InstantiateAsync(pageName);
            if (go == null) return null;

            var ui = go.GetComponent<IUserInterface>();
            if (ui == null)
            {
                Debug.LogError($"UIFactory: '{pageName}' has no IUserInterface component.");
                AssetManager.ReleaseInstance(go);
                return null;
            }

            ui.PageName = pageName;

            // 3) Cache policy comes from the prefab itself
            if (ui.Cacheable)
                _cache[pageName] = ui;

            return ui;
        }
    }
}
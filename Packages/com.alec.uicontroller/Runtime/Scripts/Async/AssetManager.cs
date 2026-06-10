using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace Elka.UI.Controller
{
    public class AssetManager : MonoBehaviour
    {

        private static AssetManager Instance;
        private void Awake()
        {
            if (Instance)
                ReleaseInstance(gameObject);
            else
            {
                Instance = this;
          //      DontDestroyOnLoad(gameObject);
            }
        }


        public static GameObject InstantiateAsync(string assetName)
        {
            return Addressables.InstantiateAsync(assetName).WaitForCompletion();
        }
        public static bool ReleaseInstance(GameObject t)
        {
            if (t)
            return Addressables.ReleaseInstance(t);
            else
                return false;
        }


    }
}
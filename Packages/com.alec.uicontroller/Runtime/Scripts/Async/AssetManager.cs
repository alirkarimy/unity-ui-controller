using System;
using System.Threading.Tasks;
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


        public static async Task<GameObject> InstantiateAsync(string assetName)
        {
            var handle = Addressables.InstantiateAsync(assetName);
            await handle.Task;
            return handle.Result;
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
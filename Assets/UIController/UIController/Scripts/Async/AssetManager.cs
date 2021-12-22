using UnityEngine;
#if async
using UnityEngine.AddressableAssets;
#endif
namespace Elka.UI.Controller
{
    public class AssetManager : MonoBehaviour
    {

        // Reference to prefabs of IUserInterfaces. handled by DialogeRefrencesInspector.cs custom Editor
        [HideInInspector]
        public UserInterface[] baseDialogs;

        private static AssetManager Instance;
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
        public static IUserInterface Instantiate(UIType type)
        {
            IUserInterface dialog = Instance.baseDialogs[(int)type];

            if (dialog == null) return null;
            if (dialog.GetInstantiatable() == null) return null;

            return Instantiate(dialog.GetInstantiatable()).GetComponent<IUserInterface>();
        }



        public static GameObject InstantiateAsync(string assetName)
        {
#if async
            return Addressables.InstantiateAsync(assetName).WaitForCompletion();
#else
            Debug.LogError("Async mode is not activated, Please first Add Addressable Package or Develope your own package and then try again");
            return null;
#endif

        }
        public static bool ReleaseInstance(GameObject t)
        {
            if (t)
#if async
            return Addressables.ReleaseInstance(t);
#else
            { Destroy(t); return true; }
#endif
            else
                return false;
        }


    }
}
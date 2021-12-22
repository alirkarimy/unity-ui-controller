using UnityEngine;

namespace Elka.UI.Controller
{
    public interface IUserInterface
    {
     
        void Show();
        void Hide();
        void Close();
        bool EnableEscape { get; }

        GameObject GetInstantiatable();
        void Destroy();

        bool PersistentWhileSceneChanges{ get; }
      
        UIType Type { set; get; }
        UIShowType ShowType { set; get; }

        Canvas GetCanvas();
    }
}
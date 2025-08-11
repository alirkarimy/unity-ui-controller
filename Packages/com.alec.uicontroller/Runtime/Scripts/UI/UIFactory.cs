using UnityEngine;

namespace Elka.UI.Controller
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
        public IUserInterface GetUI(string pageName)
        {
            IUserInterface ui = AssetManager.Instantiate(pageName);
            ui.PageName = pageName;
            return ui;
        }

         /// <summary>
        /// Creating new async instance of prefab.
        /// </summary>
        /// <returns>New instance of prefab.</returns>
        public IUserInterface GetUIAsync(string pageName)
        {
            IUserInterface ui = AssetManager.InstantiateAsync(pageName).GetComponent<IUserInterface>();
            ui.PageName = pageName;
            return ui;
        }


    }

}
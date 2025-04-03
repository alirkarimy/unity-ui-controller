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
        public IUserInterface GetUI(UIType type)
        {
            IUserInterface ui = AssetManager.Instantiate(type);
            ui.Type = type;
            return ui;
        }

         /// <summary>
        /// Creating new async instance of prefab.
        /// </summary>
        /// <returns>New instance of prefab.</returns>
        public IUserInterface GetUIAsync(UIType type)
        {
            IUserInterface ui = AssetManager.InstantiateAsync(type.ToString()).GetComponent<IUserInterface>();
            ui.Type = type;
            return ui;
        }


    }

}
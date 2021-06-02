using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIController
{
    public interface IUIFactory
    {
        IUserInterface GetUI(UIType type);

        IAsyncOperation<GameObject> GetUIAsync(UIType type);

    }

}

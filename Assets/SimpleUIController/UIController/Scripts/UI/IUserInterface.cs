using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIController
{
    public interface IUserInterface
    {
        void InitializeCallbacks(Action<IUserInterface> onOpen, Action<IUserInterface> onClose);

        void Show();
        void Hide();
        void Close();
        bool EnableEscape { get; }

        GameObject GetInstantiatable();
        void Destroy();

        UIType GetType();
        void SetShowType(UIShowType showType);
        UIShowType GetShowType();

        Canvas GetCanvas();
    }
}
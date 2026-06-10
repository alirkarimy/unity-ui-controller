using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elka.UI.Controller
{
    public interface IUserInterface : IEqualityComparer<IUserInterface>
    {

        void Show();
        void Hide();
        void Close();
        bool EnableEscape { get; }

        GameObject GetInstantiatable();
        void Destroy();

        bool PersistentWhileSceneChanges { get; }
        bool hasOverlayBackground { get; }

        string PageName { set; get; }
        UIShowType ShowType { set; get; }

        Canvas GetCanvas();
        UICloseMode CloseMode { get; }
        bool Cacheable { get; }

        void ResetVisualState();

    }
}
public enum UICloseMode
{
    ReleaseInstance, // رفتار فعلی
    HideOnly         // برای UIهای Cacheable
}


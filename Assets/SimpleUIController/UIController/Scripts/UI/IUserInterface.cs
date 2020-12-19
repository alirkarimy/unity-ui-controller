using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserInterface 
{
    void InitializeCallbacks(Action<IUserInterface> onOpen, Action<IUserInterface> onClose);

    void Show();
    void Hide();
    void Close();

    GameObject GetInstantiatable();
    void Destroy();

    UIType GetType();
}

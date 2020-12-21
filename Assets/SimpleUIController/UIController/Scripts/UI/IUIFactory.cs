﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIFactory
{
    IUserInterface GetUI(UIType type);
#if async
    IAsyncOperation<GameObject> GetUIAsync(UIType type);
#endif
}
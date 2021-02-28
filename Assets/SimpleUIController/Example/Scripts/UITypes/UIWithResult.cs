using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIWithResult : UserInterface
{
    /// <summary>
    /// This callback returns the user interaction result with an integer number
    /// 
    ///  /// <param name="result">
    ///  1 = user agreement 
    ///  0 = user dissagreement 
    ///  -1 = user canceled
    /// </param>
    /// </summary>
    public UnityAction<int> onResult;
    public GameObject title, message;

    public virtual void OnYesClick()
    {
        if (onResult != null) onResult.Invoke(1);
        
        Close();
    }

    public virtual void OnNoClick()
    {
        if (onResult != null) onResult.Invoke(0);

        Close();
    }


    public virtual void OnCancelClick()
    {
        if (onResult != null) onResult.Invoke(-1);

        Close();
    }
}

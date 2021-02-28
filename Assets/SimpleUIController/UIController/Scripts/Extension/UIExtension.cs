﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class UIExtension 
{
    public static void ShowYesNoDialog(this UIController uIController, string title, string content, UnityAction<int> onResult)
    {
        var dialog = (UIWithResult)uIController.GetDialog(UIType.UIWithResult);

        if (!dialog) return;

        if (dialog.title != null) dialog.title.SetText(title);
        if (dialog.message != null) dialog.message.SetText(content);
        if (onResult != null) dialog.onResult += onResult;
        uIController.ShowDialog(dialog, UIShowType.REPLACE_CURRENT);
    }
   

    public static void ShowOkDialog(this UIController uIController, string title, string content, UnityAction<int> onResult)
    {
        var dialog = (UIWithResult)uIController.GetDialog(UIType.UIWithResult);

        if (dialog.title != null) dialog.title.SetText(title);
        if (dialog.message != null) dialog.message.SetText(content);
        if (onResult != null) dialog.onResult += onResult;
        uIController.ShowDialog(dialog, UIShowType.REPLACE_CURRENT);
    }


    public static void SetText(this GameObject obj, string value)
    {
        Text text = obj.GetComponent<Text>();

        if (text != null)
        {
            text.text = value;
        }
        else
        {
            TextMeshProUGUI tmpro = obj.GetComponent<TextMeshProUGUI>();
            if (tmpro != null)
            {
                tmpro.text = value;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIController : MonoBehaviour
{
    public UIType type;
    private UIType prevType;

    [SerializeField] ButtonShowUI buttonShowUI;

    private void Start()
    {
        buttonShowUI.SetText(string.Format("Show {0}", type.ToString()));
        
    }

    public void ShowUI()
    {
#if async
        UIController.instance.ShowDialogAsync(type, UIShowType.STACK);
#else
        UIController.instance.ShowDialog(type, UIShowType.STACK);
#endif
    }

    public void CloseCurrentUI()
    {
        UIController.instance.CloseCurrentDialog();
    }

    private void Update()
    {
        if (type == prevType) return;
        prevType = type;
        buttonShowUI.SetText(string.Format("Show {0}", type.ToString()));
    }
}

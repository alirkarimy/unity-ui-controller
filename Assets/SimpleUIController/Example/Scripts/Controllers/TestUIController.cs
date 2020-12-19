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
        UIController.instance.ShowDialog(type, UIShowType.STACK);
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

#region enum

public enum UIShowType
{
    DONT_SHOW_IF_OTHERS_SHOWING,
    REPLACE_CURRENT,
    STACK,
    SHOW_PREVIOUS,// this type will only use inside the UIController for showing stacked UserInterfaces
    OVER_CURRENT
};
#endregion

[RequireComponent(typeof(IUIFactory))]
public class UIController : MonoBehaviour
{

    #region Refrences

    public static UIController instance;

    public IUIFactory uiFactory;

    [HideInInspector]
    public IUserInterface currentWindow;

    public Stack<IUserInterface> dialogs = new Stack<IUserInterface>();
  
    #endregion

    #region callbacks

    public Action OnScreenClear;
   
    #endregion

	public void Awake()
	{
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        uiFactory = GetComponent<IUIFactory>();
    }

    private void OnOneDialogOpened(IUserInterface dialog)
    {

    }

    private void OnOneDialogClosed(IUserInterface dialog)
    {
        if (currentWindow == dialog)
        {
            currentWindow = null;
            dialogs.Pop();
            if (OnScreenClear != null && dialogs.Count == 0)
                OnScreenClear();

            if (dialogs.Count > 0)
            {
                ShowDialog(dialogs.Peek(), UIShowType.SHOW_PREVIOUS);
            }
        }
    }

    #region Show Dialog

    #region Sync

    public void ShowDialog(int type)
	{
		ShowDialog((UIType)type, UIShowType.DONT_SHOW_IF_OTHERS_SHOWING); 
	}

    public void ShowDialog(UIType type, UIShowType option = UIShowType.REPLACE_CURRENT)
    {
        IUserInterface dialog = GetDialog(type);
        ShowDialog(dialog, option);
    }

   
    public IUserInterface GetDialog(UIType type)
    {
        return uiFactory.GetUI(type); ;
    }
    public void ShowDialog(IUserInterface ui, UIShowType option = UIShowType.OVER_CURRENT) 
	{
        if (ui == null) return;

        if (currentWindow != null)
        {
            if (option == UIShowType.DONT_SHOW_IF_OTHERS_SHOWING)
            {
                ui.Destroy();
                return;
            }
            else if (option == UIShowType.REPLACE_CURRENT)
            {
                currentWindow.Close();
            }
            else if (option == UIShowType.STACK)
            {
                currentWindow.Hide();
            }
        }

        currentWindow = ui;
        if (option != UIShowType.SHOW_PREVIOUS)
            dialogs.Push(currentWindow);

        currentWindow.InitializeCallbacks(OnOneDialogOpened, OnOneDialogClosed);

        currentWindow.Show();

        //if (onDialogsOpened != null)
        //    onDialogsOpened();


    }

    #endregion

    #region Async
#if async
   
     public void ShowDialogAsync(UIType type, UIShowType option = UIShowType.REPLACE_CURRENT)
    {
        StartCoroutine(LoadDialogAsync(type,option));
    }
    IEnumerator LoadDialogAsync(UIType type,UIShowType option)
    {

        IAsyncOperation<GameObject> asyncOperation = uiFactory.GetUIAsync(type);

        if (!asyncOperation.IsValid) yield break;

        while (!asyncOperation.IsDone)
            yield return null;

        if (asyncOperation.Result == null) yield break;

        IUserInterface dialog = asyncOperation.Result.GetComponent<IUserInterface>();

        if (dialog == null) yield break;
        if (dialog.GetInstantiatable() == null) yield break;
        ShowDialog(dialog,option);
    }
#endif
    #endregion

    #endregion

    #region Close Dialog

    public void CloseCurrentDialog()
	{
		if (currentWindow != null)
			currentWindow.Close();
	}

    public void CloseDialog(UIType type)
    {
        if (currentWindow == null) return;
        if (currentWindow.GetType() == type)
        {
            currentWindow.Close();
        }
    }

#endregion

#region DialogsStatus

    public bool IsDialogShowing()
	{
		return currentWindow != null;
	}

    public bool IsDialogShowing(UIType type)
    {
        if (currentWindow == null) return false;
        return currentWindow.GetType() == type;
    }
   
#endregion

}

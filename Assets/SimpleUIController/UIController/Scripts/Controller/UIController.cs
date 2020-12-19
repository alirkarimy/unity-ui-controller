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
	public static UIController instance;


    #region UI Refrences    
    public IUIFactory uiFactory;
    #endregion

    #region variables
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

    public void ShowDialog(int type)
	{
		ShowDialog((UIType)type, UIShowType.DONT_SHOW_IF_OTHERS_SHOWING); 
	}

	public void ShowDialog(UIType type, UIShowType option = UIShowType.REPLACE_CURRENT)
	{
        IUserInterface dialog = GetDialog(type);
		ShowDialog(dialog, option);
	}

    //public void ShowYesNoDialog(string title, string content, UnityAction onYesListener, UnityAction onNoListenter, DialogType yesNoDialog = DialogType.YesNo, DialogShow option = DialogShow.REPLACE_CURRENT)
    //{
    //    var dialog = (YesNoDialog)GetDialog(yesNoDialog);
    //    if (!dialog)
    //        dialog = (YesNoDialog)GetDialog(DialogType.YesNo);
    //    if (dialog.title != null) dialog.title.SetText(title);
    //    if (dialog.message != null) dialog.message.SetText(content);
    //    if (onYesListener != null) dialog.onYesClick += onYesListener;
    //    if (onNoListenter != null) dialog.onNoClick += onNoListenter;
    //    ShowDialog(dialog, option);
    //}
    //public void ShowTutorialDialog(TutorialSettings dialogSettings, UnityAction OnNextClick, UnityAction OnSkipClick, DialogShow option = DialogShow.REPLACE_CURRENT)
    //{
    //    var dialog = (TutorialDialog)GetDialog(DialogType.Tutorial);
    //    if (OnNextClick != null) dialog.onNextClick += OnNextClick;
    //    if (OnSkipClick != null) dialog.onSkipClick += OnSkipClick;
    //    if (dialog.title != null) dialog.title.SetText(dialogSettings.title);
    //    if (dialog.message != null) dialog.message.SetText(dialogSettings.message);
    //    dialog.settings = dialogSettings;
    //    ShowDialog(dialog, option);
    //}

 //   public void ShowOkDialog(string title, string content, Action onOkListener, DialogShow option = DialogShow.REPLACE_CURRENT)
	//{
	//	var dialog = (OkDialog)GetDialog(DialogType.Ok);
 //       if (dialog.title != null) dialog.title.SetText(title);
	//	if (dialog.message != null) dialog.message.SetText(content);
	//	dialog.onOkClick = onOkListener;
	//	ShowDialog(dialog, option);
	//}

	public void ShowDialog(IUserInterface ui, UIShowType option = UIShowType.REPLACE_CURRENT) 
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

    public IUserInterface GetDialog(UIType type)
	{
        return uiFactory.GetUI(type);
        //      IUserInterface dialog = baseDialogs.GetUI(type);

        //      if (dialog == null) return null;
        //      if (dialog.GetInstantiatable() == null) return null;

        //return Instantiate(dialog.GetInstantiatable(), transform.position, transform.rotation).GetComponent<IUserInterface>();
    }

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

	public bool IsDialogShowing()
	{
		return currentWindow != null;
	}

    public bool IsDialogShowing(UIType type)
    {
        if (currentWindow == null) return false;
        return currentWindow.GetType() == type;
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

}

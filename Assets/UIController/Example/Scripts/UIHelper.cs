using Elka.UI.Controller;
using Elka.UI.Controller.Example;
using UnityEngine.Events;


public static class UIHelper
{
    public static void ShowYesNoDialog<T>(string title, string content, UnityAction<T> onResult, UIShowType showType)
    {
        UIWithResult<T> dialog = (UIWithResult<T>)UIController.GetDialogAsync(UIType.YesNoPopup);

        if (!dialog) return;

        dialog.FullFill(title, content, onResult);

        UIController.ShowDialog(dialog, showType);
    }


    public static void ShowOkDialog<T>(string title, string content, UnityAction<int> onResult, UIShowType showType)
    {
        OkPopup dialog = (OkPopup)UIController.GetDialogAsync(UIType.OkPopup);
        if (!dialog) return;

        dialog.FullFill(title, content, onResult);

        UIController.ShowDialog(dialog, showType);
    }


}

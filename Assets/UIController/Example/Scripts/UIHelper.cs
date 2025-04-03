using Elka.UI.Controller;
using Elka.UI.Controller.Example;
using UnityEngine.Events;


public static class UIHelper
{
    public static void ShowYesNoDialog(string title, string content, UnityAction<YesNoPopup.Result> onResult, UIShowType showType)
    {
        YesNoPopup dialog = (YesNoPopup)UIController.GetDialogAsync(UIType.YesNoPopup);

        if (!dialog) return;

        dialog.FullFill(title, content, onResult);

        UIController.ShowDialog(dialog, showType);
    }


    public static void ShowOkDialog(string title, string content, UnityAction<OkPopup.Result> onResult, UIShowType showType)
    {
        OkPopup dialog = (OkPopup)UIController.GetDialogAsync(UIType.OkPopup);
        if (!dialog) return;

        dialog.FullFill(title, content, onResult);

        UIController.ShowDialog(dialog, showType);
    }


}

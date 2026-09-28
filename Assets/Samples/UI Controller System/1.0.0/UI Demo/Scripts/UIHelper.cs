using Elka.UI.Controller;
using Elka.UI.Controller.Example;
using UnityEngine.Events;


public static class UIHelper
{
    public static async void ShowYesNoDialog(string title, string content, UnityAction<YesNoPopup.Result> onResult, UIShowType showType)
    {
        YesNoPopup dialog = (YesNoPopup)await UIController.GetDialogAsync(UIType.YesNoPopup.ToString());

        if (!dialog) return;

        dialog.FullFill(title, content, onResult);

        UIController.ShowDialog(dialog, showType);
    }


    public static async void ShowOkDialog(string title, string content, UnityAction<OkPopup.Result> onResult, UIShowType showType)
    {
        OkPopup dialog = (OkPopup)await UIController.GetDialogAsync(UIType.OkPopup.ToString());
        if (!dialog) return;

        dialog.FullFill(title, content, onResult);

        UIController.ShowDialog(dialog, showType);
    }


}

namespace Elka.UI.Controller.Example
{
    public class Settings : UserInterface
    {
        public async void ShowYesNoPoup()
        {
            YesNoPopup yesNo = (YesNoPopup)await UIController.GetDialogAsync(UIType.YesNoPopup.ToString());
            if (yesNo == null) return;
            yesNo.FullFill("From Settings", "Please Choose Yes or No", ShowOkPoup);
            UIController.ShowDialog(yesNo, UIShowType.OVER_CURRENT);
        }

        public async void ShowOkPoup(YesNoPopup.Result result)
        {
            OkPopup ok = (OkPopup)await UIController.GetDialogAsync(UIType.OkPopup.ToString());
            if (ok == null) return;
            string resultText = result == YesNoPopup.Result.Yes ? "Yes" : "No";
            ok.FullFill("Yes No Result", $"You clicked on {resultText}", null);
            UIController.ShowDialog(ok, UIShowType.OVER_CURRENT);
        }
    }
}
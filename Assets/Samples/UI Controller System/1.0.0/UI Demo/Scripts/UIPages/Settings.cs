namespace Elka.UI.Controller.Example
{
    public class Settings : UserInterface
    {
        public void ShowYesNoPoup()
        {
            YesNoPopup yesNo = UIController.GetDialogAsync(UIType.YesNoPopup.ToString()) as YesNoPopup;
            yesNo.FullFill("From Settings", "Please Choose Yes or No", ShowOkPoup);
            UIController.ShowDialog(yesNo, UIShowType.OVER_CURRENT);
        }

        public void ShowOkPoup(YesNoPopup.Result result)
        {
            OkPopup ok = UIController.GetDialogAsync(UIType.OkPopup.ToString()) as OkPopup;
            string resultText = result == YesNoPopup.Result.Yes ? "Yes" : "No";
            ok.FullFill("Yes No Result", $"You clicked on {resultText}", null);
            UIController.ShowDialog(ok, UIShowType.OVER_CURRENT);
        }
    }
}
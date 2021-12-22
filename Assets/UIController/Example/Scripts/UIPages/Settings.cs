namespace Elka.UI.Controller.Example
{
    public class Settings : UserInterface
    {
        public void ShowYesNoPoup()
        {
            YesNoPopup yesNo = UIController.GetDialogAsync(UIType.YesNoPopup) as YesNoPopup;
            yesNo.FullFill("From Settings", "Please Choose Yes or No", ShowOkPoup);
            UIController.ShowDialog(yesNo, UIShowType.OVER_CURRENT);
        }

        public void ShowOkPoup(int result)
        {
            OkPopup ok = UIController.GetDialogAsync(UIType.OkPopup) as OkPopup;
            string resultText = result == 1 ? "Yes" : "No";
            ok.FullFill("Yes No Result", $"You clicked on {resultText}", null);
            UIController.ShowDialog(ok, UIShowType.OVER_CURRENT);
        }
    }
}
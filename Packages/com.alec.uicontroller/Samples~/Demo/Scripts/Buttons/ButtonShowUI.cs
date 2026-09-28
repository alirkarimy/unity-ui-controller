namespace Elka.UI.Controller.Example
{
    public class ButtonShowUI : MyButton
    {
        public UIType UIToShow;
        public UIShowType UIShowType;

        public override void OnButtonClick()
        {
            base.OnButtonClick();

            UIController.ShowDialogAsync(UIToShow.ToString(), UIShowType);
        }
    }
}
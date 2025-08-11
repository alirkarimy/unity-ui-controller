namespace Elka.UI.Controller.Example
{
    public class ButtonShowUI : MyButton
    {
        public UIType UIToShow;
        public UIShowType UIShowType;

        public override void OnButtonClick()
        {
            base.OnButtonClick();

#if !async
        UIController.ShowDialogAsync(UIToShow.ToString(), UIShowType);
#else
            UIController.ShowDialog(UIToShow.ToString(), UIShowType);
#endif

        }
    }
}
namespace Elka.UI.Controller.Example
{
    public class ButtonCloseUI : MyButton
    {
        public override void OnButtonClick()
        {
            base.OnButtonClick();
            UIController.CloseCurrentDialog();

        }

    }
}
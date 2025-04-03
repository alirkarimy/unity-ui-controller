namespace Elka.UI.Controller.Example
{
    public class BackButton : MyButton
    {
        public override void OnButtonClick()
        {
            base.OnButtonClick();
            GetComponent<GotoScene>().LoadScene();
        }
    }
}
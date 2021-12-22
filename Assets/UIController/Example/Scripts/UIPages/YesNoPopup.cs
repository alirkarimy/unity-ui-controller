namespace Elka.UI.Controller.Example
{
    public class YesNoPopup : UIWithResult<int>
    {
        public override void SetResultData(int data)
        {
            this.resultData = data;
        }

        public void YesButtonClicked()
        {
            SetResultData(1);
            Close();
        }
        public void NoButtonClicked()
        {
            SetResultData(-1);
            Close();
        }


    }
}
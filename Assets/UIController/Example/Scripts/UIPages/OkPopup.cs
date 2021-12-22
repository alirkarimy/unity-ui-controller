namespace Elka.UI.Controller.Example
{
    public class OkPopup : UIWithResult<int>
    {
        public override void SetResultData(int data)
        {
            this.resultData = data;
        }

        public void OkButtonClicked()
        {
            SetResultData(1);
            Close();
        }

    }
}
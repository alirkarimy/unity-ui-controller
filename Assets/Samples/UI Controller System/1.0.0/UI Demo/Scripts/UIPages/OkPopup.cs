namespace Elka.UI.Controller.Example
{
    public class OkPopup : UIWithResult<OkPopup.Result>
    {
        public enum Result
        {
            Ok = 1,
            Close = 0
        }
        public override void SetResultData(OkPopup.Result data)
        {
            this.resultData = data;
        }

        public void OkButtonClicked()
        {
            SetResultData(this.resultData);
            Close();
        }

    }
}
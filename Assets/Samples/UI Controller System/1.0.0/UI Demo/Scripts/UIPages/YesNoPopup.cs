namespace Elka.UI.Controller.Example
{
    public class YesNoPopup : UIWithResult<YesNoPopup.Result>
    {
        public enum Result
        {
            Yes,
            No,
            Close
        }
        public override void SetResultData(Result data)
        {
            this.resultData = data;
        }

        public void YesButtonClicked()
        {
            SetResultData(Result.Yes);
            Close();
        }
        public void NoButtonClicked()
        {
            SetResultData(Result.No);
            Close();
        }


    }
}
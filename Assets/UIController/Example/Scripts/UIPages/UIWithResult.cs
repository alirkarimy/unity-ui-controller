using UnityEngine;
using UnityEngine.Events;
namespace Elka.UI.Controller.Example
{
    public abstract class UIWithResult<T> : UserInterface 
    {
        /// <summary>
        /// This callback returns the user interaction result with user demand result type
        /// <param name="resultData">
        /// </param>
        /// </summary>
        protected UnityEvent _resultCallback = new UnityEvent();
        
        [SerializeField]protected GameObject titleText, messageText;

       
        protected T resultData = default;
        
        public abstract void SetResultData(T data);
     
        public void FullFill(string title, string message, UnityAction<T> onResult)
        {
            if (title != null) titleText.SetText(title);
          
            if (message != null) messageText.SetText(message);

            if (onResult != null) 
            {
                _resultCallback?.RemoveAllListeners();
                _resultCallback?.AddListener(delegate { onResult(resultData); });
            }
        }
        public override void Close()
        {
            base.Close();
            _resultCallback?.Invoke();
        }
    }
}
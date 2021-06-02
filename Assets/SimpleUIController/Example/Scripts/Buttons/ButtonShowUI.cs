using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UIController.Example
{
    public class ButtonShowUI : MyButton
    {
        public UIType UIToShow;
        public UIShowType UIShowType;

        public override void OnButtonClick()
        {
            base.OnButtonClick();

#if async
        UIController.instance.ShowDialogAsync(UIToShow, UIShowType);
#else
            UIController.instance.ShowDialog(UIToShow, UIShowType);
#endif

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UIController.Example
{
    public class ButtonCloseUI : MyButton
    {
        public override void OnButtonClick()
        {
            base.OnButtonClick();
            UIController.instance.CloseCurrentDialog();

        }

    }
}
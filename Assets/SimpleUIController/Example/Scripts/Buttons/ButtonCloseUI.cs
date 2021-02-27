using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonCloseUI : MyButton
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        UIController.instance.CloseCurrentDialog();

    }

}

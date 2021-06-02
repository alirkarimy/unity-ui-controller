using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIController.Example
{
    public class Settings : UserInterface
    {
        public void OnSaveClick()
        {
            UIController.instance.ShowYesNoDialog("Change Settings", "Are you sure to save changes ?", OnPopupResult, UIShowType.STACK);
        }

        private void OnPopupResult(int arg0)
        {
            string message = "You Canceled";
            switch (arg0)
            {
                case 0:
                    message = "Changes Discarded";
                    break;
                case 1:
                    message = "Changes Saved";
                    break;
            }
            UIController.instance.ShowOkDialog("Change Settings", message, OnOKPopupResult, UIShowType.REPLACE_CURRENT);
        }

        private void OnOKPopupResult(int a)
        {
            UIController.instance.CloseCurrentDialog();
        }
    }
}
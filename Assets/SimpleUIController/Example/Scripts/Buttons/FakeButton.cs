using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UIController.Example
{
    public class FakeButton : MyButton
    {
        [SerializeField] private TextMeshProUGUI fakeText;
        private bool isTextOn { get { return fakeText.gameObject.activeInHierarchy; } }
        public override void OnButtonClick()
        {
            if (isTextOn) return;

            base.OnButtonClick();
            ShowText();
            Timer.Schedule(this, 1, HideText);
        }

        private void ShowText()
        {
            fakeText.gameObject.SetActive(true);
        }
        private void HideText()
        {
            fakeText.gameObject.SetActive(false);
        }
    }
}
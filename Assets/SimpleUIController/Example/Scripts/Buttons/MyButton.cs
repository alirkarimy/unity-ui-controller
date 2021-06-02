using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
namespace UIController.Example
{
    public class MyButton : MonoBehaviour
    {

        protected Button button;

        protected virtual void Start()
        {
            button = GetComponent<Button>() ?? gameObject.AddComponent<Button>();

            button.onClick.AddListener(OnButtonClick);

        }

        public virtual void OnButtonClick()
        {
            //TODO : Play Sound effect
        }
    }
}
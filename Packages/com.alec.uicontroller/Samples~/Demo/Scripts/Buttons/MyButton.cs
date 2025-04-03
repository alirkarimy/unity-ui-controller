using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Elka.UI.Controller.Example
{
    public class MyButton : MonoBehaviour
    {

        protected Button button;
        [SerializeField] protected TextMeshProUGUI _text;

        protected virtual void Start()
        {
            button = GetComponent<Button>() ?? gameObject.AddComponent<Button>();

            button.onClick.AddListener(OnButtonClick);

        }

        public virtual void OnButtonClick()
        {
            //TODO : Play Sound effect
        }

        public virtual void SetText(string name)
        {
            if(!string.IsNullOrEmpty(name)) _text?.SetText(name);
        }
    }
}
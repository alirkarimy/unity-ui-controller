using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Elka.UI.Controller.Example
{
    public class MyButton : MonoBehaviour
    {

        protected Button button;
        [SerializeField] protected TextMeshProUGUI _text;

        protected virtual void Start()
        {
            if (button == null)
                button = GetComponent<Button>() ?? gameObject.AddComponent<Button>();

            AddListener(OnButtonClick);

        }

        public void AddListener(UnityAction onClick)
        {
            if (button == null)
                button = GetComponent<Button>() ?? gameObject.AddComponent<Button>();

            button.onClick.AddListener(onClick);

        }
        public virtual void OnButtonClick()
        {
            //TODO : Play Sound effect
        }

        public virtual void SetText(string name)
        {
            if (!string.IsNullOrEmpty(name)) _text?.SetText(name);
        }
    }
}
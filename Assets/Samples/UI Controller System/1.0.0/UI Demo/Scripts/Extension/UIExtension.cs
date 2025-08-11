using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Elka.UI.Controller.Example
{
    public static class UIExtension
    {

        public static void SetText(this GameObject obj, string value)
        {
            Text text = obj.GetComponent<Text>();

            if (text != null)
            {
                text.text = value;
            }
            else
            {
                TextMeshProUGUI tmpro = obj.GetComponent<TextMeshProUGUI>();
                if (tmpro != null)
                {
                    tmpro.text = value;
                }else
                    Debug.Log("Text not found");

            }
        }

        public static string GetText(this GameObject obj)
        {
            Text text = obj.GetComponent<Text>();

            if (text != null)
            {
                return text.text;
            }
            else
            {
                TextMeshProUGUI tmpro = obj.GetComponent<TextMeshProUGUI>();
                if (tmpro != null)
                {
                    return tmpro.text;
                }
            }
            return string.Empty;
        }
    }
}
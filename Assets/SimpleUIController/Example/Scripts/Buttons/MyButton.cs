using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class MyButton : MonoBehaviour
{

    protected Button button;
    [SerializeField] TextMeshProUGUI mText;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
        if (!button)
            button = gameObject.AddComponent<Button>() ?? gameObject.AddComponent<Button>();

        button.onClick.AddListener(OnButtonClick);
        
    }

    public void SetText(string text)
    {
        if (mText) mText.text = text;
    }
    public virtual void OnButtonClick()
    {
        //TODO : Play Sound effect
    }    	
}

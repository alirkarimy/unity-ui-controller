using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Window : UserInterface
{
    [SerializeField]private Image image;
    
    public override void Show()
    {
        base.Show();
        if (image) image.color = Color.red;
      
    }
    
}

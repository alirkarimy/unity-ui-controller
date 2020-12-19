using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modal : UserInterface
{
    [SerializeField] private Image image;

    public override void Show()
    {
        base.Show();
        if (image) image.color = Color.green;

    }

}

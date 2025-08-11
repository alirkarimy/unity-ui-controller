﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public string sceneName;
    public bool useScreenTransition;
    public bool useKeyCode;
    public KeyCode keyCode;

    public virtual void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (useKeyCode && Input.GetKeyDown(keyCode))
        {
            LoadScene();
        }
    }
}
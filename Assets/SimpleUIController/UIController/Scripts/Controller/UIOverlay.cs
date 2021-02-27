using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIOverlay : MonoBehaviour
{
    private Image overlay;
    private Button overlayButton;

    private void Awake()
    {
        overlay = GetComponent<Image>();
        overlayButton = GetComponent<Button>() ?? gameObject.AddComponent<Button>();
    }

    private void Start()
    {
        UIController.onDialogOpen += OnDialogOpened;
        UIController.onDialogClose+= OnDialogClosed;
        overlayButton.onClick.RemoveAllListeners();
        overlayButton.onClick.AddListener(UIController.instance.CloseCurrentDialog);
    }

    private void OnDialogOpened(IUserInterface obj)
    {
        overlay.enabled = true;

        if (obj.EnableEscape)
            overlayButton.interactable = true;
        else
            overlayButton.interactable = false;
    }

    private void OnDialogClosed(IUserInterface obj)
    {
        overlay.enabled = false;
    }

 

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        overlay.enabled = false;
    }

    private void OnDestroy()
    {
        UIController.onDialogOpen -= OnDialogOpened;
        UIController.onDialogClose -= OnDialogClosed;
    }
}

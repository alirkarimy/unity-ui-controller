using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIOverlay : MonoBehaviour
{
    private Image overlay;
    private Button overlayButton;
    private Canvas mCanvas;

    private void Awake()
    {
        overlay = GetComponent<Image>();
        overlayButton = GetComponent<Button>() ?? gameObject.AddComponent<Button>();
        mCanvas = GetComponent<Canvas>();
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
        Debug.Log(0);
        overlay.enabled = true;

        if (obj.EnableEscape)
            overlayButton.interactable = true;
        else
            overlayButton.interactable = false;

        mCanvas.sortingLayerID = obj.GetCanvas().sortingLayerID;
        mCanvas.sortingLayerName = obj.GetCanvas().sortingLayerName;
        mCanvas.sortingOrder = obj.GetCanvas().sortingOrder - 1;
        
    }

    private void OnDialogClosed(IUserInterface obj)
    {
        Debug.Log(1);

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

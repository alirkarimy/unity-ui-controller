using UnityEngine;
using UnityEngine.UI;
namespace Elka.UI.Controller
{
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
            overlayButton.onClick.RemoveAllListeners();
            overlayButton.onClick.AddListener(UIController.CloseCurrentDialog);
        }

        private void OnDialogOpened(IUserInterface obj)
        {
            overlay.enabled = true;

            if (obj.EnableEscape)
                overlayButton.interactable = true;
            else
                overlayButton.interactable = false;

            mCanvas.sortingLayerID = obj.GetCanvas().sortingLayerID;
            mCanvas.sortingLayerName = obj.GetCanvas().sortingLayerName;
            mCanvas.sortingOrder = obj.GetCanvas().sortingOrder - 1;

        }

        private void OnDialogClosed()
        {
            overlay.enabled = false;
        }



        private void OnEnable()
        {
            UIController.onDialogOpen += OnDialogOpened;
            UIController.OnScreenClear += OnDialogClosed;

        }

        private void OnDisable()
        {
            UIController.onDialogOpen -= OnDialogOpened;
            UIController.OnScreenClear -= OnDialogClosed;

        }

     
    }
}
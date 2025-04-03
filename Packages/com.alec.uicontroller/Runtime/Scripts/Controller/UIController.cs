using UnityEngine;
using System.Collections.Generic;
using System;

namespace Elka.UI.Controller
{
    #region enum

    public enum UIShowType
    {
        DONT_SHOW_IF_OTHERS_SHOWING,
        REPLACE_CURRENT,
        STACK,
        SHOW_PREVIOUS,//Warning : Don't use this outside of UIController 
        OVER_CURRENT
    };
    #endregion

    [RequireComponent(typeof(IUIFactory))]
    public class UIController : MonoBehaviour
    {

        #region Refrences

        private static IUIFactory _uiFactory;
        private static IUIFactory uiFactory
        {
            get
            {
                if (_uiFactory!=null)
                    return _uiFactory;
                else
                {   
                    GameObject uiController = new GameObject("UIController");
                    _uiFactory = uiController.AddComponent<UIFactory>();
                    uiController.AddComponent<AssetManager>();
                    uiController.AddComponent<UIController>();
                    return _uiFactory;
                }
            }
        }

        private static Stack<IUserInterface> dialogs = new Stack<IUserInterface>();

        public static IUserInterface currentWindow;
        public static int CurrentWindowSortOrder { get { return dialogs.Count; } }
        #endregion

        #region callbacks

        public static Action<IUserInterface> onDialogOpen;
        public static Action<IUserInterface> onDialogStartClosing;
        public static Action<IUserInterface> onAnimationIn;
        public static Action<IUserInterface> onAnimationOut;
        public static Action OnScreenClear;

        #endregion

        private void Awake()
        {
            if (_uiFactory != GetComponent<IUIFactory>())
                Destroy(gameObject);
        }
        private void OnEnable()
        {
            onDialogOpen += OnOneDialogOpened;
            onDialogStartClosing += OnOneDialogClosed;
            onAnimationIn += OnOneDialogShow;
            onAnimationOut += OnOneDialogHide;
        }
        private void OnDisable()
        {
      //      if (Instance != this) return; // preventing from unsubscribing subscribed events by the instance

            onDialogOpen -= OnOneDialogOpened;
            onDialogStartClosing -= OnOneDialogClosed;
            onAnimationIn -= OnOneDialogShow;
            onAnimationOut -= OnOneDialogHide;
        }

        private void OnDestroy()
        {
            _uiFactory = null;
            dialogs.Clear();
            currentWindow = null;
    }

        private static void OnOneDialogOpened(IUserInterface dialog)
        {
            Debug.Log($"open {dialog?.Type}");
        }

        private static void OnOneDialogClosed(IUserInterface dialog)
        {
            Debug.Log($"close {dialog?.Type}");

           
            if (dialogs.Count == 0)
                return;

            if (dialogs.Peek().Type == dialog.Type)
            {
                dialogs.Pop();
            }

        }

        private static void OnOneDialogShow(IUserInterface dialog)
        {
            Debug.Log($"Show {dialog?.Type}");
        }

        private static void OnOneDialogHide(IUserInterface dialog)
        {
            Debug.Log($"Hide {dialog?.Type}");
            if (dialogs.Count > 0)
            {
                ShowDialog(dialogs.Peek(), UIShowType.SHOW_PREVIOUS);
            }
            else
            {
                currentWindow = null;
                OnScreenClear?.Invoke();
            }


        }

        #region Show Dialog

        #region Prepare Dialog in Sync mode

        public static void ShowDialog(UIType type, UIShowType option = UIShowType.REPLACE_CURRENT)
        {
            IUserInterface dialog = GetDialog(type);
            ShowDialog(dialog, option);
        }

        public static IUserInterface GetDialog(UIType type)
        {
            return uiFactory.GetUI(type);
        }

        #endregion

        #region Prepare Dialog in Async mode


        public static void ShowDialogAsync(UIType type, UIShowType option = UIShowType.REPLACE_CURRENT)
        {
            IUserInterface dialog = GetDialogAsync(type);
            ShowDialog(dialog, option);
        }
        public static IUserInterface GetDialogAsync(UIType type)
        {
            return uiFactory.GetUIAsync(type);
        }

        #endregion

        public static void ShowDialog(IUserInterface ui, UIShowType option = UIShowType.OVER_CURRENT)
        {
            if (ui == null) return;

            if (currentWindow != null)
            {
                if (option == UIShowType.DONT_SHOW_IF_OTHERS_SHOWING)
                {
                    ui.Destroy();
                    return;
                }
                else if (option == UIShowType.REPLACE_CURRENT)
                {
                    currentWindow.Close();
                }
                else if (option == UIShowType.STACK)
                {
                    currentWindow.Hide();
                }
            }
            if (option != UIShowType.SHOW_PREVIOUS)
                dialogs.Push(ui);


            currentWindow = ui;
            ui.Show();
        }

        #endregion

        #region Close Dialog

        public static void CloseCurrentDialog()
        {
            if (currentWindow != null)
                currentWindow.Close();
        }

        public static void CloseDialog(UIType type)
        {

            if (currentWindow == null) return;
            if (currentWindow.Type == type)
            {
                currentWindow.Close();
            }
        }

        #endregion

        #region DialogsStatus

        public static bool IsDialogShowing()
        {
            return currentWindow != null;
        }

        public static bool IsDialogShowing(UIType type)
        {
            if (currentWindow == null) return false;
            return currentWindow.Type == type;
        }

        #endregion

    }
}
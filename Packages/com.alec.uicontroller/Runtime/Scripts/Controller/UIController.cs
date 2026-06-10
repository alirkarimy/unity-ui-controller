using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

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
                if (_uiFactory != null)
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

        public static float TransitionDelay = 0.15f;

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
            Debug.Log($"open {dialog?.PageName}");
        }

        private static void OnOneDialogClosed(IUserInterface dialog)
        {
            Debug.Log($"close {dialog?.PageName}");

            if (dialogs.Count > 0 && dialogs.Peek().PageName == dialog.PageName)
                dialogs.Pop();


            if (dialogs.Count > 0)
            {
                Timer.Schedule(uiFactory as MonoBehaviour, TransitionDelay, () =>
                        {
                            ShowDialog(dialogs.Peek(), UIShowType.SHOW_PREVIOUS);
                        });
            }
        }

        private static void OnOneDialogShow(IUserInterface dialog)
        {
            Debug.Log($"Show {dialog?.PageName}");
        }

        private static void OnOneDialogHide(IUserInterface dialog)
        {
            Debug.Log($"Hide {dialog?.PageName}");

            if (currentWindow != null
                && !currentWindow.PageName.Equals(dialog.PageName, StringComparison.OrdinalIgnoreCase))
                return;

            if (dialogs.Count == 0)
            {
                currentWindow = null;
                OnScreenClear?.Invoke();
            }
        }

        #region Show Dialog


        #region Prepare Dialog in Async mode


        public static void ShowDialogAsync(string pageName, UIShowType option = UIShowType.REPLACE_CURRENT)
        {
            IUserInterface dialog = GetDialogAsync(pageName);
            ShowDialog(dialog, option);
        }
        public static IUserInterface GetDialogAsync(string pageName)
        {
            return uiFactory.GetUIAsync(pageName);
        }

        #endregion

        public static void ShowDialog(IUserInterface ui, UIShowType option = UIShowType.OVER_CURRENT)
        {
            if (ui == null || ui.Equals(currentWindow)) return;

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

            // اگر پنل در stack وجود داشته باشد، آن را پیدا کرده و به بالای stack بیاوریم
            if (dialogs.Contains(ui))
            {
                // پنل A را پیدا کنیم و به بالای stack منتقل کنیم
                // اینجا باید پنل را از stack برداریم و دوباره به بالای stack اضافه کنیم
                Stack<IUserInterface> tempStack = new Stack<IUserInterface>();
                while (dialogs.Count > 0)
                {
                    IUserInterface dialog = dialogs.Pop();
                    if (dialog != ui)
                        tempStack.Push(dialog);  // نگه داشتن باقی‌مانده‌ی پنل‌ها
                    else
                        break;  // وقتی پنل A پیدا شد، دیگر ادامه نمی‌دهیم
                }


                // باقی‌مانده‌ی پنل‌ها را دوباره به stack برمی‌گردانیم
                while (tempStack.Count > 0)
                {
                    dialogs.Push(tempStack.Pop());
                }
                // حالا پنل A را به بالای stack اضافه می‌کنیم
                dialogs.Push(ui);

            }
            else if (option != UIShowType.SHOW_PREVIOUS)
                dialogs.Push(ui);
            // پنل A را نمایش می‌دهیم
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

        public static void CloseDialog(string pageName)
        {

            if (currentWindow == null) return;
            if (currentWindow.PageName.Equals(pageName, StringComparison.OrdinalIgnoreCase))
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

        public static bool IsDialogShowing(string pageName)
        {
            if (currentWindow == null) return false;
            return currentWindow.PageName.Equals(pageName, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

    }
}
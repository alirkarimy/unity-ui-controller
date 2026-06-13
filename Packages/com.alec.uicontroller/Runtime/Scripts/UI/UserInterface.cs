using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elka.UI.Controller
{
    public class UserInterface : MonoBehaviour, IUserInterface
    {

        #region callbacks

        //public Action<IUserInterface> onStart;
        //public Action<IUserInterface> onClosed;
        //public Action<IUserInterface> onAnimationIn;
        //public Action<IUserInterface> onAnimationOut;

        #endregion

        #region Variables
        protected IAnimator animator;
       
        [SerializeField] private bool _isPersistent = false;
        public bool enableAd = true;
        public bool enableEscape = true;
        public bool animationIn = true;
        public bool animationOut = true;
     
        public bool _overlayBackground = true;

        protected bool isShowing;
        #endregion

        #region Inspector Area
        private string mPageName;
        private UIShowType mShowType;
        [SerializeField] private UICloseMode closeMode = UICloseMode.ReleaseInstance;

        #endregion

        protected virtual void Awake()
        {
            if (animator == null) animator = GetComponent<IAnimator>();
            animationIn = animationIn && animator != null;
            animationOut = animationOut && animator != null;

        }
       
        protected virtual void Start()
        {
            if (GetComponent<Canvas>().renderMode != RenderMode.ScreenSpaceOverlay)
                GetComponent<Canvas>().worldCamera = Camera.main;
            if (PersistentWhileSceneChanges)
                DontDestroyOnLoad(gameObject);

        }

        protected virtual void Update()
        {
            if (enableEscape && isShowing)
            {
                var cancelAction = InputSystem.actions?.FindAction("Cancel");
                if (cancelAction != null && cancelAction.triggered)
                {
                    Close();
                }
            }
        }
     
     

        private void OnEscape()
        {
            if (enableEscape)
            {
                Close();
            }
        }

        public bool IsShowing()
        {
            return isShowing;
        }

        public void PlaySFX()
        {
            // TODD : Play Sound Effects
        }


        #region IUserInterface

        public virtual void Show()
        {
            ResetVisualState();

            GetInstantiatable().SetActive(true);
            GetCanvas().sortingOrder = UIController.CurrentWindowSortOrder;
            isShowing = true;

            Timer.Schedule(this, Time.deltaTime * 2,
                () =>
                {
                    if (animationIn)
                    {
                        animator.SetTrigger("show");
                        Timer.Schedule(this, animator.GetCurrentAnimationLenght(),
                            () =>
                            {
                                //OnAnimation In End
                               UIController.onAnimationIn?.SafeInvoke(this);
                            }
                            );

                    }
                    else
                        UIController.onAnimationIn?.SafeInvoke(this);

                    if (enableAd)
                    {
                        //TODO : Send Show Ad Request here
                    }
                    else
                        Debug.Log("intestetial ad disabled here");

                });

            UIController.onDialogOpen?.SafeInvoke(this);



        }

        public virtual void Hide()
        {
            isShowing = false;

            if (animationOut && animator.HasTrigger("hide"))
            {
                animator.SetTrigger("hide");
                Timer.Schedule(this, animator.GetCurrentAnimationLenght(),            
                       () => {
                           UIController.onAnimationOut?.SafeInvoke(this);
                           GetInstantiatable().SetActive(false); 
                       }
                  );
            }
            else
            {
                Timer.Schedule(this, 0,
                       () => {
                           UIController.onAnimationOut?.SafeInvoke(this);
                           GetInstantiatable().SetActive(false); 
                       }
                  );

            }
        }

        public virtual void Close()
        {
            isShowing = false;
            UIController.onDialogStartClosing?.SafeInvoke(this);
            if (animationOut && animator.HasTrigger("hide"))
            {
                animator.SetTrigger("hide");
                Timer.Schedule(this, animator.GetCurrentAnimationLenght(),
                        @Destroy
                  );
            }
            else
            {
                Timer.Schedule(this, 0,                   
                        @Destroy
                  );

            }

        }


        public bool EnableEscape { get { return enableEscape; } }

        public GameObject GetInstantiatable()
        {
            return gameObject;
        }

        public virtual void @Destroy()
        {
            
            UIController.onAnimationOut?.SafeInvoke(this);
            if (Cacheable)
            {
                GetInstantiatable().SetActive(false);
            }else
                AssetManager.ReleaseInstance(GetInstantiatable());
            
        }

        public string PageName
        {
            get
            {
                return mPageName;
            }
            set
            {
                mPageName = value;
            }
        }

        public UIShowType ShowType
        {
            get
            {
                return mShowType;
            }
            set
            {
                mShowType = value;
            }
        }

        public bool PersistentWhileSceneChanges => _isPersistent;

        public bool hasOverlayBackground => _overlayBackground;

        public UICloseMode CloseMode => closeMode;

        public bool Cacheable => CloseMode == UICloseMode.HideOnly;

        public Canvas GetCanvas()
        {
            return GetComponent<Canvas>();
        }

        public void ResetVisualState()
        {
            //if (_rt != null)
            //{
            //    _rt.anchoredPosition = _initialAnchoredPos;
            //    _rt.localScale = _initialLocalScale;
            //    _rt.localRotation = _initialLocalRot;
            //}

            if (animator != null)
            {
                // Reset animator to default state (prevents being stuck in "hidden" pose)
                animator.Rebind();
                animator.UpdateTime(0f);
            }
        }

        public bool Equals(IUserInterface x, IUserInterface y)
        {
            return !string.IsNullOrEmpty(x.PageName) || !string.IsNullOrEmpty(y.PageName) || x.PageName.Equals(y.PageName);
        }

        public int GetHashCode(IUserInterface obj)
        {
            return HashCode.Combine(mPageName, name);
        }
        #endregion
    }
}
using UnityEngine;
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
        private IAnimator animator;

        [SerializeField] private bool _isPersistent = false;
        public bool enableAd = true;
        public bool enableEscape = true;
        public bool animationIn = true;
        public bool animationOut = true;

        protected bool isShowing;
        #endregion

        #region Inspector Area
        private UIType mType;
        private UIShowType mShowType;

        #endregion

        protected virtual void Awake()
        {
            if (animator == null) animator = GetComponent<IAnimator>();
            animationIn = animationIn && animator != null;
            animationOut = animationOut && animator != null;

        }

        protected virtual void Start()
        {
            if (GetComponent<Canvas>().renderMode != RenderMode.ScreenSpaceOverlay) GetComponent<Canvas>().worldCamera = Camera.main;
            if (PersistentWhileSceneChanges)
                DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {

            if (enableEscape && Input.GetKeyDown(KeyCode.Escape))
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
                               UIController.onAnimationIn?.Invoke(this);
                            }
                            );

                    }
                    else
                        UIController.onAnimationIn?.Invoke(this);

                    if (enableAd)
                    {
                        //TODO : Send Show Ad Request here
                    }
                    else
                        Debug.Log("intestetial ad disabled here");

                });

            UIController.onDialogOpen?.Invoke(this);



        }

        public virtual void Hide()
        {
            GetInstantiatable().SetActive(false);
            isShowing = false;
        }

        public virtual void Close()
        {
            isShowing = false;
            UIController.onDialogStartClosing(this);
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
            
            UIController.onAnimationOut?.Invoke(this);

            AssetManager.ReleaseInstance(GetInstantiatable());
            
        }

        public UIType Type
        {
            get
            {
                return mType;
            }
            set
            {
                mType = value;
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

        public Canvas GetCanvas()
        {
            return GetComponent<Canvas>();
        }
        #endregion
    }
}
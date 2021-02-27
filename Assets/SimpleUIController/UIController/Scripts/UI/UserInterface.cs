using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface :MonoBehaviour, IUserInterface
{

    #region callbacks
    public Action<IUserInterface> onOpened;
    public Action<IUserInterface> onClosed;
    #endregion

    #region Variables
    private IAnimator animator;

    public bool enableAd = true;
    public bool enableEscape = true;
    private bool isShowing;
    #endregion

    #region Inspector Area
    private UIType mType;
    private UIShowType mShowType;

    #endregion

    protected virtual void Awake()
    {
        if (animator == null) animator = GetComponent<IAnimator>();
    }

    protected virtual void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
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
        isShowing = true;

        Timer.Schedule(this, Time.deltaTime * 2, 
            () => 
            {
                if (animator != null && animator.HasTrigger("show"))
                {
                    animator.SetTrigger("show");
                }

            });
        
        if (onOpened != null) onOpened(this);

        if (enableAd)
        {
            //TODO : Send Show Ad Request here
        }else
            Debug.Log("intestetial ad disabled here");

    }

    public virtual void Hide()
    {
        GetInstantiatable().SetActive(false);
        isShowing = false;
    }

    public virtual void Close()
    {
        isShowing = false;
        if (animator != null && animator.HasTrigger("hide"))
        {
            animator.SetTrigger("hide");
            Timer.Schedule(this, animator.GetCurrentAnimationLenght(), @Destroy);
        }
        else
        {
            @Destroy();
        }

        if (onClosed != null) onClosed(this);
    }

    public void InitializeCallbacks(Action<IUserInterface> onOpen, Action<IUserInterface> onClose)
    {
        onOpened = onOpen;
        onClosed = onClose;
    }

    public bool EnableEscape { get { return enableEscape; } }

    public GameObject GetInstantiatable()
    {
        return gameObject;
    }

    public void @Destroy()
    {
        AssetManager.ReleaseInstance(GetInstantiatable());
    }

    UIType IUserInterface.GetType()
    {
        return mType;
    }

    public UIShowType GetShowType()
    {
        return mShowType;
    }

    public void SetShowType(UIShowType showType)
    {
        mShowType = showType;
    }
    #endregion
}

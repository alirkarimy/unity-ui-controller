using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIController
{
    public class AnimatorClass : MonoBehaviour, IAnimator
    {
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            if (!animator) animator = GetComponent<Animator>();
        }


        #region IAnimator methods
        public float GetCurrentAnimationLenght()
        {
            if (!animator)
            {
                Debug.LogError("Animator is not attached");
                return 0;
            }
            try
            {
                return animator.GetCurrentAnimatorStateInfo(0).length;
            }
            catch { return 0; }

        }

        public void SetTrigger(string parameter)
        {
            if (!animator)
            {
                Debug.LogError("Animator is not attached");
                return;
            }
            animator.SetTrigger(parameter);
        }

        public bool HasTrigger(string triggerName)
        {
            if (!animator)
            {
                Debug.LogError("Animator is not attached");
                return false;
            }
            AnimatorControllerParameter[] parameters = animator.parameters;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].type == AnimatorControllerParameterType.Trigger && parameters[i].name.Equals(triggerName)) return true;
            }
            return false;
        }
        #endregion
    }
}
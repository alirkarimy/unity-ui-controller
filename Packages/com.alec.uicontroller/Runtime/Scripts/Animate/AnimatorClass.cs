using UnityEngine;
namespace Elka.UI.Controller
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
                Debug.Log("Animator is not attached");
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
                Debug.Log("Animator is not attached");
                return;
            }

            if (HasTrigger(parameter))
                animator.SetTrigger(parameter);
            else
                Debug.Log($"Animator has no trigger named {parameter}");

        }

        public bool HasTrigger(string triggerName)
        {
            if (!animator)
            {
                Debug.Log("Animator is not attached");
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
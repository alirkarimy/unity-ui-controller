namespace Elka.UI.Controller
{
    public interface IAnimator
    {
        bool HasTrigger(string stateName);
        void SetTrigger(string parameter);
        float GetCurrentAnimationLenght();
    }
}
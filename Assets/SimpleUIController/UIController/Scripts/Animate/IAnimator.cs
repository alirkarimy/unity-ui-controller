using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIController
{
    public interface IAnimator
    {
        bool HasTrigger(string stateName);
        void SetTrigger(string parameter);
        float GetCurrentAnimationLenght();
    }
}
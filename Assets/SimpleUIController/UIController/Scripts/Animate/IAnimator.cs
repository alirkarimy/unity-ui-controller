using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimator 
{
    bool HasTrigger(string stateName);
    void SetTrigger(string parameter);
    float GetCurrentAnimationLenght();
}

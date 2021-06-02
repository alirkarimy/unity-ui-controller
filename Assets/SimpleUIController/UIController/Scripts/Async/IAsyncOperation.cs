using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIController
{
    public interface IAsyncOperation<T>
    {
        bool IsValid { get; }
        bool IsDone { get; }
        T Result { get; }
    }
}
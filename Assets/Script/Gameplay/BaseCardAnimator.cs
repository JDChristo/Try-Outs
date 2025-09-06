using System;
using UnityEngine;

public abstract class BaseCardAnimator : MonoBehaviour
{
    public abstract void Matched();
    public abstract void Show(Action onComplete = null);
    public abstract void Hide(Action onComplete = null);
}

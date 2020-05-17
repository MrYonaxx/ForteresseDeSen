using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField]
    UnityEvent eventAnim;

    public void CallEvent()
    {
        eventAnim.Invoke();
    }
}

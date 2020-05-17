using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame : MonoBehaviour
{

    [SerializeField]
    protected UnityEvent eventEnd;

    public virtual void InitializeMinigame()
    {
        
    }


    public void SetEndMinigame(UnityAction call)
    {
        eventEnd.AddListener(call);
    }

    public void EndMinigame()
    {
        eventEnd.Invoke();
        eventEnd.RemoveAllListeners();
    }
}

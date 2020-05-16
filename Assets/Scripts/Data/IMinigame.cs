using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;


public interface IMinigame
{
    void InitializeMinigame();
    void EndMinigame();

    void SetEndMinigame(UnityAction call);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MinigamesManager : SerializedMonoBehaviour
{
    [SerializeField]
    IMinigame[] minigames;

    public void StartMinigame()
    {
        minigames[0].SetEndMinigame(NextMinigame);
    }

    public void NextMinigame()
    {

    }

}

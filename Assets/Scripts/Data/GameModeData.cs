using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "GameModeData", menuName = "GameModeData", order = 1)]
public class GameModeData : ScriptableObject
{
    [SerializeField]
    int minigameNumber;
    public int MinigameNumber
    {
        get { return minigameNumber; }
        set { minigameNumber = value; }
    }


    [SerializeField]
    private string[] playerName;
    public string[] PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }


    [SerializeField]
    int maxHealth;

}

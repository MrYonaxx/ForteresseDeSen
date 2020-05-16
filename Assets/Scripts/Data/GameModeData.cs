using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "GameModeData", menuName = "GameModeData", order = 1)]
public class GameModeData : ScriptableObject
{
    [SerializeField]
    int minigameNumber;


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

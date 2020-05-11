using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameModeData", menuName = "GameModeData", order = 1)]
public class GameModeData : ScriptableObject
{
    [SerializeField]
    int minigameNumber;

    /*[SerializeField]
    int minigameNumber;*/

    [SerializeField]
    int maxHealth;

}

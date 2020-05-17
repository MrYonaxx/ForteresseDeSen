using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameButtonMashManager : Minigame
{

    [SerializeField]
    GameModeData gameModeData;
    [SerializeField]
    int minigameLevel = 1;

    [Header("Animator")]
    [SerializeField]
    Animator city1;
    [SerializeField]
    Animator city2;
    [SerializeField]
    Animator city3;
    [SerializeField]
    Animator ground;

    [Header("ButtonMash")]
    [SerializeField]
    MinigameButtonMashPlayer player;
    [SerializeField]
    MinigameButtonMashEnemy enemy;

    private void Start()
    {
        InitializeMinigame();
    }

    public override void InitializeMinigame()
    {
        StartCoroutine(InitializeCoroutine());
    }

    private IEnumerator InitializeCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        enemy.Active();
        player.Active();
    }
}

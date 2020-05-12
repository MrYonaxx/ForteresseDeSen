using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBasketManager : MonoBehaviour, IMinigame
{

    [Header("Database")]
    [SerializeField]
    GameBasketData[] minigameQuizzDatabase;
    [SerializeField]
    GameModeData gameModeData;
    [SerializeField]
    int minigameDifficultyLevel = 1;



    [SerializeField]
    GameBasketObject[] gameBasketObjectPrefab;

    [Header("Player")]
    [SerializeField]
    CursorController[] players;

    public Question currentQuestion;
    List<MinigameAnswerButton> listButtons = new List<MinigameAnswerButton>();


    public void InitializeMinigame()
    {

    }

}

public class GameBasket
{
    public GameBasketData Data;

    public List<int> ObjectToShoot;
    public List<int> Pattern;

    public GameBasket(GameBasketData gameBasketData)
    {
        int total = Random.Range(gameBasketData.NumberObjectShootMin, gameBasketData.NumberObjectShootMax);
        int amount = 0;
        while (amount < total)
        {
            int r = Random.Range(0, gameBasketData.Patterns.Length);
            Pattern.Add(r);
            for(int i = 0; i < gameBasketData.Patterns[r].TurretPatterns.Length; i++)
            {
                if(gameBasketData.Patterns[r].TurretPatterns[i].ShootNumber <= 0)
                    amount += gameBasketData.Patterns[r].TurretPatterns[i].ShootTime.Length;
                else
                    amount += gameBasketData.Patterns[r].TurretPatterns[i].ShootNumber;
            }

        }

        for(int i = 0; i < gameBasketData.GameBasketObjectDatabase.Length; i++)
        {
            int r = Random.Range(gameBasketData.GameBasketObjectDatabase[i].NumberToCollectMin, gameBasketData.GameBasketObjectDatabase[i].NumberToCollectMax);
            for (int j = 0; j < r; j++)
            {
                ObjectToShoot.Add(i);
            }
        }

        while(ObjectToShoot.Count < amount)
        {
            ObjectToShoot.Add(Random.Range(0, gameBasketData.GameBasketObjectDatabase.Length));
        }

        Data = gameBasketData;
    }
}

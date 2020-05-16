using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class GameBasketManager : MonoBehaviour, IMinigame
{

    [Header("Database")]
    [SerializeField]
    GameBasketData[] gameBasketDatabase;
    [SerializeField]
    GameModeData gameModeData;
    [SerializeField]
    [Min(1)]
    int minigameDifficultyLevel = 1;

    [SerializeField]
    Transform transformParent;
    [SerializeField]
    float fruitYoffset = 1.7f;
    [SerializeField]
    GameBasketObject gameBasketObjectPrefab;

    [Header("Draw")]
    [SerializeField]
    Image[] imageObjectToCollect;
    [SerializeField]
    TextMeshProUGUI[] textObjectToCollect;


    [SerializeField]
    UnityEvent eventEnd;

    private int currentShootedObject;
    public GameBasket currentGameBasketData;

    private void Start()
    {
        InitializeMinigame();
    }

    public void InitializeMinigame()
    {
        currentGameBasketData = new GameBasket(gameBasketDatabase[minigameDifficultyLevel-1]);
        DrawCollectObject();
        StartCoroutine(PatternCoroutine());
    }

    public void AddOject(int objectID)
    {
        currentGameBasketData.ObjectToCollect[objectID] -= 1;
        DrawCollectObject();
    }

    private void DrawCollectObject()
    {
        for(int i = 0; i < imageObjectToCollect.Length; i++)
        {
            if( i >= currentGameBasketData.ObjectToCollect.Count)
            {
                imageObjectToCollect[i].gameObject.SetActive(false);
                textObjectToCollect[i].gameObject.SetActive(false);
                continue;
            }
            if (currentGameBasketData.Data.GameBasketObjectDatabase[i].GameBasketPrefab != null)
            {
                imageObjectToCollect[i].sprite = currentGameBasketData.Data.GameBasketObjectDatabase[i].GameBasketPrefab.GetSprite();
                imageObjectToCollect[i].color = currentGameBasketData.Data.GameBasketObjectDatabase[i].GameBasketPrefab.GetColor();
            }
            else
            {
                imageObjectToCollect[i].sprite = currentGameBasketData.Data.GameBasketObjectDatabase[i].ObjectSprite;
            }
            textObjectToCollect[i].text = currentGameBasketData.ObjectToCollect[i].ToString();
        }
    }

    private IEnumerator PatternCoroutine()
    {
        for (int j = 0; j < currentGameBasketData.Pattern.Count; j++)
        {
            TurretBasketPattern[] pattern = currentGameBasketData.Data.Patterns[currentGameBasketData.Pattern[j]].TurretPatterns;
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i].Time != 0)
                    yield return new WaitForSeconds(pattern[i].Time);
                if(pattern[i].ShootID <= -1)
                    InstantiateProjectile(pattern[i].PositionX, currentShootedObject);
                else
                    InstantiateProjectile(pattern[i].PositionX, pattern[i].ShootID, false);
                currentShootedObject += 1;
            }
            yield return new WaitForSeconds(currentGameBasketData.Data.TimeBetweenPattern);
        }
    }

    private void InstantiateProjectile(float positionX, int index, bool objectToShootID = true)
    {
        int objectID = 0;
        if (objectToShootID == true)
            objectID = currentGameBasketData.ObjectToShoot[index];
        else
            objectID = index;

        GameBasketObject basketObject;
        if (currentGameBasketData.Data.GameBasketObjectDatabase[objectID].GameBasketPrefab != null)
        {
            basketObject = Instantiate(currentGameBasketData.Data.GameBasketObjectDatabase[objectID].GameBasketPrefab, transformParent);
            basketObject.transform.localPosition = new Vector3(positionX, fruitYoffset);
        }
        else
        {
            basketObject = Instantiate(gameBasketObjectPrefab, transformParent);
            basketObject.transform.localPosition = new Vector3(positionX, fruitYoffset, transformParent.localPosition.z);
            basketObject.CreateObject(currentGameBasketData.Data.GameBasketObjectDatabase[objectID].ObjectSprite, currentGameBasketData.Data.GameBasketObjectDatabase[objectID].Speed);
        }
        if(currentGameBasketData.Data.GameBasketObjectDatabase[objectID].IsClone == true) 
        {
            basketObject.SetID(currentGameBasketData.Data.GameBasketObjectDatabase[objectID].CloneID);
        }
        else
        {
            basketObject.SetID(objectID);
        }

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

public class GameBasket
{
    public GameBasketData Data;

    public List<int> ObjectToCollect = new List<int>();
    public List<int> ObjectToShoot = new List<int>();
    public List<int> Pattern = new List<int>();

    public GameBasket(GameBasketData gameBasketData)
    {
        int total = Random.Range(gameBasketData.NumberObjectShootMin, gameBasketData.NumberObjectShootMax);
        int amount = 0;
        while (amount < total)
        {
            int r = Random.Range(0, gameBasketData.Patterns.Length);
            Pattern.Add(r);
            amount += gameBasketData.Patterns[r].TurretPatterns.Length;

        }

        for(int i = 0; i < gameBasketData.GameBasketObjectDatabase.Length; i++)
        {
            int r = Random.Range(gameBasketData.GameBasketObjectDatabase[i].NumberToCollectMin, gameBasketData.GameBasketObjectDatabase[i].NumberToCollectMax+1);
            for (int j = 0; j < r; j++)
            {
                ObjectToShoot.Add(i);
            }
            ObjectToCollect.Add(r);
        }

        while(ObjectToShoot.Count < amount)
        {
            ObjectToShoot.Add(Random.Range(0, gameBasketData.GameBasketObjectDatabase.Length));
        }

        Data = gameBasketData;
    }
}

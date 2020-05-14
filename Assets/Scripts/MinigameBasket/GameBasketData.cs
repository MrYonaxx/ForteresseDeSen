using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

[System.Serializable]
public class GameBasketObjectData
{
    [HideIf("objectSprite", null)]
    [SerializeField]
    private GameBasketObject gameBasketPrefab;
    public GameBasketObject GameBasketPrefab
    {
        get { return gameBasketPrefab; }
    }


    [HideIf("gameBasketPrefab", null)]
    [HorizontalGroup("BasketObjectData", Width = 64)]
    [HideLabel]
    [PreviewField(ObjectFieldAlignment.Left, Height = 64)]
    [SerializeField]
    Sprite objectSprite;
    public Sprite ObjectSprite
    {
        get { return objectSprite; }
    }

    [HorizontalGroup("BasketObjectData")]
    [VerticalGroup("BasketObjectData/Right")]
    [HideIf("gameBasketPrefab", null)]
    [SerializeField]
    Vector2 speed;
    public Vector2 Speed
    {
        get { return speed; }
    }

    [HorizontalGroup("BasketObjectCollect")]
    [SerializeField]
    int numberToCollectMin;
    public int NumberToCollectMin
    {
        get { return numberToCollectMin; }
    }

    [HorizontalGroup("BasketObjectCollect")]
    [SerializeField]
    int numberToCollectMax;
    public int NumberToCollectMax
    {
        get { return numberToCollectMax; }
    }

    [HorizontalGroup("BasketObjectCollect2")]
    [SerializeField]
    bool isClone;
    public bool IsClone
    {
        get { return isClone; }
    }
    [ShowIf("isClone")]
    [HorizontalGroup("BasketObjectCollect2")]
    [ValueDropdown("GetAllTypeName")]
    [SerializeField]
    int cloneID;
    public int CloneID
    {
        get { return cloneID; }
    }

    private static List<string> GameDatabase = new List<string>();

    public IList<ValueDropdownItem<int>> GetAllTypeName()
    {
        var res = new ValueDropdownList<int>();
        for (int i = 0; i < GameDatabase.Count; i++)
            res.Add(GameDatabase[i], i);
        return res;
    }

    public void SetGameDatabase(GameBasketObjectData[] gameBasketObjectDatabase)
    {
        GameDatabase.Clear();
        for (int i = 0; i < gameBasketObjectDatabase.Length; i++)
        {
            if (gameBasketObjectDatabase[i].GameBasketPrefab != null)
                GameDatabase.Add(gameBasketObjectDatabase[i].GameBasketPrefab.name);
            else if (gameBasketObjectDatabase[i].ObjectSprite != null)
                GameDatabase.Add(gameBasketObjectDatabase[i].ObjectSprite.name);
        }
    }
}

[System.Serializable]
public class PatternBasketData
{
    [HorizontalGroup("Pattern", Width = 150)]
    [HideLabel]
    [SerializeField]
    string note;

    [HorizontalGroup("Pattern")]
    [SerializeField]
    TurretBasketPattern[] turretPatterns;
    public TurretBasketPattern[] TurretPatterns
    {
        get { return turretPatterns; }
    }
}

[System.Serializable]
public class TurretBasketPattern 
{
    [HorizontalGroup("Turret", LabelWidth = 100)]
    [SerializeField]
    float positionX;
    public float PositionX
    {
        get { return positionX; }
    }

    [HorizontalGroup("Turret")]
    [SerializeField]
    float time;
    public float Time
    {
        get { return time; }
    }

    [HorizontalGroup("Turret")]
    [SerializeField]
    [HideLabel]
    [ValueDropdown("GetAllTypeName")]
    int shootID = -1;
    public int ShootID
    {
        get { return shootID; }
    }

    private static List<string> GameDatabase = new List<string>();

    public IList<ValueDropdownItem<int>> GetAllTypeName()
    {
        var res = new ValueDropdownList<int>();
        res.Add("Random", -1);
        for(int i = 0; i < GameDatabase.Count; i++)
            res.Add(GameDatabase[i], i);
        return res;
    }

    public void SetGameDatabase(GameBasketObjectData[] gameBasketObjectDatabase)
    {
        GameDatabase.Clear();
        for (int i = 0; i < gameBasketObjectDatabase.Length; i++)
        {
            if(gameBasketObjectDatabase[i].GameBasketPrefab != null)
                GameDatabase.Add(gameBasketObjectDatabase[i].GameBasketPrefab.name);
            else if(gameBasketObjectDatabase[i].ObjectSprite != null)
                GameDatabase.Add(gameBasketObjectDatabase[i].ObjectSprite.name);
        }
    }

}

[CreateAssetMenu(fileName = "BasketPatternData", menuName = "MinigameData/BasketPatternData", order = 1)]
public class GameBasketData : ScriptableObject
{

    [Title("Parameter")]
    [SerializeField]
    string difficultyName;

    [HorizontalGroup]
    [SerializeField]
    int numberObjectShootMin;
    public int NumberObjectShootMin
    {
        get { return numberObjectShootMin; }
    }

    [HorizontalGroup]
    [HideLabel]
    [SerializeField]
    int numberObjectShootMax;
    public int NumberObjectShootMax
    {
        get { return numberObjectShootMax; }
    }


    [SerializeField]
    float timeBetweenPattern;
    public float TimeBetweenPattern
    {
        get { return timeBetweenPattern; }
    }

    [Space]

    [Space]
    [Title("Object Database")]
    [OnInspectorGUI("SetTurretDatabase", true)]
    [SerializeField]
    GameBasketObjectData[] gameBasketObjectDatabase;
    public GameBasketObjectData[] GameBasketObjectDatabase
    {
        get { return gameBasketObjectDatabase; }
    }

    public void SetTurretDatabase()
    {
        if (gameBasketObjectDatabase.Length != 0)
        {
            gameBasketObjectDatabase[0].SetGameDatabase(gameBasketObjectDatabase);
        }

        for (int i = 0; i < patterns.Length; i++)
        {
            for (int j = 0; j < patterns[i].TurretPatterns.Length; j++)
            {
                patterns[i].TurretPatterns[j].SetGameDatabase(gameBasketObjectDatabase);
                return;
            }
        }

    }

    [Space]
    [Space]
    [Title("Pattern Database")]
    [SerializeField]
    PatternBasketData[] patterns;
    public PatternBasketData[] Patterns
    {
        get { return patterns; }
    }
}

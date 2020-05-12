using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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

    [Space]
    [HorizontalGroup("BasketObjectData")]
    [VerticalGroup("BasketObjectData/Right")]
    [HideIf("gameBasketPrefab", null)]
    [SerializeField]
    Vector2 speed;
    public Vector2 Speed
    {
        get { return speed; }
    }

    [HorizontalGroup("BasketObjectData")]
    [VerticalGroup("BasketObjectData/Right")]
    [HideIf("gameBasketPrefab", null)]
    [SerializeField]
    int numberToCollectMin;
    public int NumberToCollectMin
    {
        get { return numberToCollectMin; }
    }

    [HorizontalGroup("BasketObjectData")]
    [VerticalGroup("BasketObjectData/Right")]
    [HideIf("gameBasketPrefab", null)]
    [SerializeField]
    int numberToCollectMax;
    public int NumberToCollectMax
    {
        get { return numberToCollectMax; }
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
    float startPosition;
    public float StartPosition
    {
        get { return startPosition; }
    }

    [HorizontalGroup("Turret")]
    [SerializeField]
    float endPosition;
    public float EndPosition
    {
        get { return endPosition; }
    }

    [HorizontalGroup("Turret")]
    [SerializeField]
    float time;
    public float Time
    {
        get { return time; }
    }

    [HorizontalGroup("ShootTime", LabelWidth = 100, Width = 200)]
    [SerializeField]
    int shootNumber = -1;
    public int ShootNumber
    {
        get { return shootNumber; }
    }

    [HorizontalGroup("ShootTime")]
    [ShowIf("shootNumber", -1)]
    [SerializeField]
    float[] shootTime;
    public float[] ShootTime
    {
        get { return shootTime; }
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

    [Space]

    [Space]
    [Title("Object Database")]
    [SerializeField]
    GameBasketObjectData[] gameBasketObjectDatabase;
    public GameBasketObjectData[] GameBasketObjectDatabase
    {
        get { return gameBasketObjectDatabase; }
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

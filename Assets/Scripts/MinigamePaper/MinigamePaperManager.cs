using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class MinigamePaperManager : Minigame
{
    [SerializeField]
    GameModeData gameModeData;
    [SerializeField]
    int minigameLevel;

    [Space]
    [Space]
    [Space]
    [Title("Players")]
    [SerializeField]
    PlayerInputManager inputManager;
    [SerializeField]
    Vector3[] playerInitialPositions;

    [Space]
    [Space]
    [Space]
    [Title("Parameter")]
    [SerializeField]
    int paperToWin = 5;
    [SerializeField]
    int paperNumber;
    [SerializeField]
    Vector2 paperSpawnVolumeX;
    [SerializeField]
    Vector2 paperSpawnVolumeY;
    //[SerializeField]
    //float angleIndulgent = 10;
    [SerializeField]
    MinigamePaperObject paperPrefab;

    [Space]
    [Space]
    [Space]
    [Title("Difficulty")]
    [SerializeField]
    Animator animatorValidate;
    [SerializeField]
    GameObject obstacle;
    [SerializeField]
    int levelValidateMoving = 2;
    [SerializeField]
    int levelObstacle = 3;

    //[SerializeField]
    //Transform paperBin;
    int id = 0;
    List<int> playerScore = new List<int>();

    //float goodAngle;

    private void SetPlayers()
    {
        for (int i = 0; i < gameModeData.PlayerName.Length; i++)
        {
            inputManager.JoinPlayer(i);
        }
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerScore.Add(0);
        MinigamePaperPlayerController player = playerInput.GetComponent<MinigamePaperPlayerController>();
        player.SetID(id);
        player.transform.SetParent(this.transform);
        player.transform.position = playerInitialPositions[id];
        id += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlayers();
        InitializeMinigame();
    }

    public override void InitializeMinigame() 
    {
        CreatePaperObject();
        /*goodAngle = Random.Range(0, 180);
        paperBin.localEulerAngles = new Vector3(0,0,goodAngle);*/
        if(minigameLevel >= levelValidateMoving)
        {
            animatorValidate.enabled = true;
        }
        if (minigameLevel >= levelObstacle)
        {
            obstacle.gameObject.SetActive(true);
        }
    }

    private void CreatePaperObject()
    {
        for(int i = 0; i < paperNumber; i++)
        {
            var paper = Instantiate(paperPrefab, new Vector3(Random.Range(paperSpawnVolumeX.x, paperSpawnVolumeX.y),this.transform.position.y + 1, Random.Range(paperSpawnVolumeY.x, paperSpawnVolumeY.y)), Quaternion.Euler(90, 0, Random.Range(0, 360)));
            paper.transform.SetParent(this.transform);
            paper.SetDefaultTransform(this.transform);
        }
    }

    /*public void CheckGoodAngle(MinigamePaperObject paper)
    {
        float finalAngle = Mathf.Abs(paper.GetAngle() - 360) % 180;
        if (finalAngle <= goodAngle + angleIndulgent && goodAngle - angleIndulgent <= finalAngle)
        {
            Destroy(paper.gameObject);
        }
    }*/

    public void AddScore(int playerID)
    {
        playerScore[playerID] += 1;
        if (playerScore[playerID] >= paperToWin)
            EndMinigame();
    }


}

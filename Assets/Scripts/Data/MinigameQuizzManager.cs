using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;
using Sirenix.OdinInspector;

public class MinigameQuizzManager : MonoBehaviour, IMinigame
{
    [Title("Database")]
    [SerializeField]
    MinigameQuizzData[] minigameQuizzDatabase;
    [SerializeField]
    GameModeData gameModeData;
    [SerializeField]
    int minigameDifficultyLevel = 1;

    [Title("Players")]
    [SerializeField]
    PlayerInputManager inputManager;
    [SerializeField]
    Vector3[] playerInitialPositions;

    [Title("Permutation Parameter")]
    [SerializeField]
    int permutationDifficultyLevel = 2;
    [SerializeField]
    Vector2 permutationInterval;

    [Header("Draw")]
    [SerializeField]
    TextMeshProUGUI textQuestion;
    [SerializeField]
    MinigameAnswerButton[] quizzButtonPrefab;

    [SerializeField]
    List<CursorController> players = new List<CursorController>();

    [Title("End Minigame")]
    [SerializeField]
    UnityEvent eventEnd;


    public Question currentQuestion;
    List<MinigameAnswerButton> listButtons = new List<MinigameAnswerButton>();

    int permutationIndex = 0;
    private IEnumerator permutationCoroutine;

    private void Start()
    {
        InitializeMinigame();
    }

    private void SetPlayers()
    {
        for(int i = 1; i < gameModeData.PlayerName.Length; i++)
        {
            inputManager.JoinPlayer(i);
        }
    }
    public void OnPlayerJoined(PlayerInput playerInput) 
    {
        players.Add(playerInput.GetComponent<CursorController>());
        players[players.Count - 1].transform.SetParent(this.transform);
        players[players.Count - 1].transform.position = playerInitialPositions[players.Count - 1];
    }





    public void InitializeMinigame()
    {
        SetPlayers();
        currentQuestion = minigameQuizzDatabase[Random.Range(0, minigameQuizzDatabase.Length)].CreateQuestion(3);
        CreateAnswerButton();
        DrawQuestion();
    }

    public void CreateAnswerButton()
    {
        minigameDifficultyLevel = Mathf.Clamp(minigameDifficultyLevel, 1, quizzButtonPrefab.Length);
        for (int i = 0; i < currentQuestion.AnswerProposition.Count; i++)
        {
            listButtons.Add(Instantiate(quizzButtonPrefab[minigameDifficultyLevel-1], this.transform));
            listButtons[i].DrawAnswerButton(currentQuestion.AnswerProposition[i], i);
        }

        if(permutationDifficultyLevel == minigameDifficultyLevel)
        {
            permutationCoroutine = PermutationCoroutine();
            StartCoroutine(permutationCoroutine);
        }
    }

    public void DrawQuestion()
    {
        textQuestion.text = currentQuestion.QuestionString;
    }

    public void CheckAnswer(int buttonID)
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].SetCanPlay(false);
        }
        for (int i = 0; i < listButtons.Count; i++)
        {
            listButtons[i].SetActive(false);
        }
        StartCoroutine(CheckAnswerCoroutine(buttonID));
    }

    private IEnumerator CheckAnswerCoroutine(int buttonID)
    {
        if (permutationCoroutine != null)
            StopCoroutine(permutationCoroutine);
        yield return new WaitForSeconds(2f);
        if(buttonID == currentQuestion.IndexAnswer)
        {
            Debug.Log("Win");
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].SetCanPlay(true);
            }
            for (int i = 0; i < listButtons.Count; i++)
            {
                listButtons[i].SetActive(true);
                listButtons[i].transform.localPosition = Vector3.zero;
            }
            if (permutationDifficultyLevel == minigameDifficultyLevel)
            {
                permutationCoroutine = PermutationCoroutine();
                StartCoroutine(permutationCoroutine);
            }
        }
    }



    private IEnumerator PermutationCoroutine()
    {
        //yield return new WaitForSeconds(Random.Range(permutationInterval.x, permutationInterval.y));
        while (true)
        {
            permutationIndex += 1;
            if (permutationIndex >= listButtons.Count)
                permutationIndex -= listButtons.Count;
            int index = permutationIndex;
            for (int i = 0; i < listButtons.Count; i++)
            {
                listButtons[i].DrawAnswerButton(currentQuestion.AnswerProposition[index], index);
                index += 1;
                if (index >= listButtons.Count)
                    index -= listButtons.Count;
            }
            yield return new WaitForSeconds(Random.Range(permutationInterval.x, permutationInterval.y));
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





[System.Serializable]
public struct Question 
{
    public string QuestionString;

    public int IndexAnswer;

    public List<string> AnswerProposition;

    public Question(string question, int index, List<string> listAnswer)
    {
        QuestionString = question;
        IndexAnswer = index;
        AnswerProposition = listAnswer;
    }
}

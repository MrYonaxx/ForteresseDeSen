using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameQuizzManager : MonoBehaviour, IMinigame
{
    [Header("Database")]
    [SerializeField]
    MinigameQuizzData[] minigameQuizzDatabase;
    [SerializeField]
    GameModeData gameModeData;
    [SerializeField]
    int minigameDifficultyLevel = 1;

    [Header("Draw")]
    [SerializeField]
    TextMeshProUGUI textQuestion;
    [SerializeField]
    MinigameAnswerButton[] quizzButtonPrefab;

    [Header("Player")]
    [SerializeField]
    CursorController[] players;

    public Question currentQuestion;
    List<MinigameAnswerButton> listButtons = new List<MinigameAnswerButton>();




    private void Start()
    {
        InitializeMinigame();//Debug
    }

    public void InitializeMinigame()
    {
        currentQuestion = minigameQuizzDatabase[Random.Range(0, minigameQuizzDatabase.Length - 1)].CreateQuestion(3);
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
    }

    public void DrawQuestion()
    {
        textQuestion.text = currentQuestion.QuestionString;
    }

    public void CheckAnswer(int buttonID)
    {
        for(int i = 0; i < players.Length; i++)
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
        yield return new WaitForSeconds(2f);
        if(buttonID == currentQuestion.IndexAnswer)
        {
            Debug.Log("Win");
        }
        else
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].SetCanPlay(true);
            }
            for (int i = 0; i < listButtons.Count; i++)
            {
                listButtons[i].SetActive(true);
                listButtons[i].transform.localPosition = Vector3.zero;
            }
        }
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

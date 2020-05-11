using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizzData", menuName = "MinigameData/Quizz", order = 1)]
public class MinigameQuizzData : ScriptableObject
{
    [SerializeField]
    private string question;
    public string Question
    {
        get { return question; }
    }


    [SerializeField]
    string[] answers;
    public string[] Answers
    {
        get { return answers; }
    }

    [SerializeField]
    string[] randomColor;
    public string[] RandomColor
    {
        get { return randomColor; }
    }

    /*[Header("Variable")]
    [SerializeField]
    Vector2 answerRange;
    public Vector2 AnswerRange
    {
        get { return answerRange; }
    }*/

    public Question CreateQuestion(int answerNumber)
    {
        List<string> finalAnswers = new List<string>(answers.Length);
        for (int i = 0; i < answers.Length; i++)
            finalAnswers.Add(answers[i]);
        while (finalAnswers.Count > answerNumber)
            finalAnswers.RemoveAt(Random.Range(0, finalAnswers.Count - 1));

        int finalIndex = Random.Range(0, finalAnswers.Count - 1);
        string finalColor = randomColor[Random.Range(0, randomColor.Length - 1)];
        string finalQuestion = question.Replace("#", "<color=" + finalColor + ">" + finalAnswers[finalIndex] + "</color>");
        string answer = answers[Random.Range(0, answers.Length)];
        return new Question(finalQuestion, finalIndex, finalAnswers);
    }
}

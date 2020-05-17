using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MinigamesManager : SerializedMonoBehaviour
{
    [SerializeField]
    GameModeData gameModeData;

    [SerializeField]
    Minigame[] minigamesDatabase;

    [Title("Drawer")]
    [SerializeField]
    Animator animatorTransition;


    Minigame currentMinigame;
    List<Minigame> minigamesList = new List<Minigame>();

    private void Start()
    {
        SelectMinigames();
        StartMinigame();
    }

    private void SelectMinigames()
    {
        for(int i = 0; i < gameModeData.MinigameNumber; i++)
        {
            minigamesList.Add(minigamesDatabase[Random.Range(0, minigamesDatabase.Length)]);
        }
    }

    public void StartMinigame()
    {
        animatorTransition.SetTrigger("Start");
        animatorTransition.SetInteger("Random", Random.Range(0, 2));
        currentMinigame = Instantiate(minigamesList[0], this.transform);
        currentMinigame.SetEndMinigame(EndMinigame);
    }

    public void EndMinigame()
    {
        Debug.Log("Allo?");
        animatorTransition.SetTrigger("End");
        animatorTransition.SetInteger("Random", Random.Range(0, 2));
    }

    public void NextMinigame()
    {
        Destroy(currentMinigame.gameObject);
        minigamesList.RemoveAt(0);
        StartCoroutine(NextMinigameCoroutine());
    }

    private IEnumerator NextMinigameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartMinigame();
    }

}

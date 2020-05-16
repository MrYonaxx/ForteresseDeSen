using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class MinigamePaperManager : MonoBehaviour, IMinigame
{

    [SerializeField]
    int minigameLevel;

    [Space]
    [SerializeField]
    int paperNumber;
    [SerializeField]
    Vector2 paperSpawnVolumeX;
    [SerializeField]
    Vector2 paperSpawnVolumeY;
    [SerializeField]
    float angleIndulgent = 10;

    [Space]
    [SerializeField]
    MinigamePaperObject paperPrefab;
    [SerializeField]
    Transform paperBin;

    [SerializeField]
    UnityEvent eventEnd;

    float goodAngle;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMinigame();
    }

    public void InitializeMinigame() 
    {
        CreatePaperObject();
        goodAngle = Random.Range(0, 180);
        paperBin.localEulerAngles = new Vector3(0,0,goodAngle);
    }

    private void CreatePaperObject()
    {
        for(int i = 0; i < paperNumber; i++)
        {
            MinigamePaperObject obj = Instantiate(paperPrefab, new Vector3(Random.Range(paperSpawnVolumeX.x, paperSpawnVolumeX.y),this.transform.position.y + 1, Random.Range(paperSpawnVolumeY.x, paperSpawnVolumeY.y)), Quaternion.Euler(90, 0, Random.Range(0, 360)));
            obj.SetOrderShadow(i);
        }
    }

    public void CheckGoodAngle(MinigamePaperObject paper)
    {
        float finalAngle = Mathf.Abs(paper.GetAngle() - 360) % 180;
        if (finalAngle <= goodAngle + angleIndulgent && goodAngle - angleIndulgent <= finalAngle)
        {
            Destroy(paper.gameObject);
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

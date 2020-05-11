using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameAnswerButton : MonoBehaviour
{
    [SerializeField]
    TextMeshPro textAnswer;
    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField]
    float speed = 1f;
    [SerializeField]
    Vector2 timeData;

    int buttonID;

    float t = 1f;
    float time = 10f;
    public Vector3 inertia = Vector3.zero;

    bool active = true;


    public void DrawAnswerButton(string answer, int buttonIndex)
    {
        this.gameObject.SetActive(true);
        textAnswer.text = answer;
        buttonID = buttonIndex;
    }

    public int GetButtonID()
    {
        return buttonID;
    }

    public void SetActive(bool b)
    {
        active = b;
        if(b == false)
        {
            t = 1f;
            rigidbody2D.velocity = Vector3.zero;
        }
        rigidbody2D.simulated = b;
    }

    private void Update()
    {
        if (active)
        {
            if (t < 1f)
            {
                t += Time.deltaTime / time;
            }
            else
            {
                t = 0f;
                inertia = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                time = Random.Range(timeData.x, timeData.y);
                rigidbody2D.velocity = inertia * speed;
            }
        }
    }


}

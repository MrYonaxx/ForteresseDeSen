using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;



[System.Serializable]
public class UnityEventFloat : UnityEvent<float>
{

}

[System.Serializable]
public class UnityEventInt : UnityEvent<int>
{

}

public class CursorController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbodyCursor;
    [SerializeField]
    float speed = 500;

    float speedX;
    float speedY;
    Vector3 move;

    List<Transform> transformCollision = new List<Transform>();

    [SerializeField]
    UnityEventInt OnEventValidate;

    bool canPlay = true;

    public void SetCanPlay(bool b)
    {
        canPlay = b;
        if (b == false) 
        {
            rigidbodyCursor.velocity = Vector3.zero;
        }
        rigidbodyCursor.simulated = b;
    }


    public void OnMove(InputValue value)
    {
        if (canPlay == false)
        {
            return;
        }
        move = value.Get<Vector2>();
        move *= speed;

        rigidbodyCursor.velocity = move;
    }
    public void OnInteraction()
    {
        if (canPlay == false)
        {
            return;
        }
        if (transformCollision.Count == 0)
            return;
        OnEventValidate.Invoke(transformCollision[0].GetComponent<MinigameAnswerButton>().GetButtonID());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interaction")
            transformCollision.Add(collision.transform);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interaction")
            transformCollision.Remove(collision.transform);
    }



}

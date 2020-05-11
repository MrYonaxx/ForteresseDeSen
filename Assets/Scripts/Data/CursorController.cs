using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



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

    [SerializeField]
    bool debugPlayer2 = false;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay == true)
        {
            Validate();
            Move();
        }
    }

    public void Move()
    {
        if(debugPlayer2 == true)
        {
            if (Mathf.Abs(Input.GetAxis("VerticalRightStick")) != 0)
            {
                speedY = Input.GetAxis("VerticalRightStick");
            }
            else
            {
                speedY = 0;
            }

            if (Mathf.Abs(Input.GetAxis("HorizontalRightStick")) != 0)
            {
                speedX = Input.GetAxis("HorizontalRightStick");
            }
            else
            {
                speedX = 0;
            }
        }
        else
        {
            if (Mathf.Abs(Input.GetAxis("Vertical")) != 0)
            {
                speedY = Input.GetAxis("Vertical");
            }
            else
            {
                speedY = 0;
            }

            if (Mathf.Abs(Input.GetAxis("Horizontal")) != 0)
            {
                speedX = Input.GetAxis("Horizontal");
            }
            else
            {
                speedX = 0;
            }
        }


        move = new Vector3(speedX, speedY);
        move *= speed;

        rigidbodyCursor.velocity = move * Time.deltaTime;
        /*characterController.Move(move * Time.deltaTime);
        characterController.Move(new Vector3(0, gravity * Time.deltaTime, 0));*/
    }

    public void Validate()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (transformCollision.Count == 0)
                return;
            OnEventValidate.Invoke(transformCollision[0].GetComponent<MinigameAnswerButton>().GetButtonID()); 
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameBasketPlayerController : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rigidbodyCursor;
    [SerializeField]
    float speed = 10;

    [SerializeField]
    int playerID = 0;

    float speedX;
    Vector3 move;
    bool canPlay = true;

    [SerializeField]
    UnityEventInt OnEventPlayerEat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay == true)
        {
            Move();
        }
    }

    public void Move()
    {
        if (playerID == 2)
        {
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
            if (Mathf.Abs(Input.GetAxis("Horizontal")) != 0)
            {
                speedX = Input.GetAxis("Horizontal");
            }
            else
            {
                speedX = 0;
            }
        }


        move = new Vector3(speedX, 0);
        move *= speed;

        rigidbodyCursor.velocity = move;// (move * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Interaction")
        {
            Destroy(collision.gameObject);
            OnEventPlayerEat.Invoke(playerID);
        }
    }
}

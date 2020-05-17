using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class UnityEventPaperObject : UnityEvent<MinigamePaperObject>
{

}

public class MinigamePaperPlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    float speed = 20;
    //[SerializeField]
    //float rotateSpeed = 20;

    //[SerializeField]
    //UnityEventPaperObject OnValidate;

    //float speedX;
    //float speedY;
    Vector3 move;
    float rotation;

    MinigamePaperObject draggedPaper;

    //bool onTriggerValidate = false;
    int playerID = 0;
    bool canPlay = true;


    public void SetID(int id)
    {
        playerID = id;
    }

    public void SetCanPlay(bool b)
    {
        canPlay = b;
        if (b == false)
        {
            rigidbody.velocity = Vector3.zero;
        }
        //rigidbody.simulated = b;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay == true)
        {
            Rotate();
        }
    }

    public void OnMove(InputValue value)
    {
        move = new Vector3(value.Get<Vector2>().x * speed, 0, value.Get<Vector2>().y * speed);
        rigidbody.velocity = move;
    }





    public void OnBumperLeft(InputValue value)
    {
        /*if(value.isPressed == true)
        {
            rotation += rotateSpeed;
        }
        else
        {
            rotation -= rotateSpeed;
        }*/
    }

    public void OnBumperRight(InputValue value)
    {
        /*if (value.isPressed == true)
        {
            rotation -= rotateSpeed;
        }
        else
        {
            rotation += rotateSpeed;
        }*/
    }

    public void Rotate()
    {
        this.transform.Rotate(new Vector3(0, 0, rotation * Time.deltaTime));
    }



    public void OnInteraction()
    {
        if (draggedPaper != null)
        {
            draggedPaper.ReleaseObject(move);
            draggedPaper = null;
        }
        else
        {
            int layerMask = 1 << 11;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, layerMask) == true)
            {
                draggedPaper = hit.transform.GetComponent<MinigamePaperObject>();
                draggedPaper.HoldObject(this.transform, playerID);
            }
        }
    }


    /*public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Interaction")
        {
            onTriggerValidate = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Interaction")
        {
            onTriggerValidate = false;
        }
    }*/


}

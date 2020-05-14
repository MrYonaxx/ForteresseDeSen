using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinigamePaperPlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    float speed = 20;
    [SerializeField]
    float rotateSpeed = 20;

    float speedX;
    float speedY;
    Vector3 move;
    float rotation;

    MinigamePaperObject draggedPaper;

    bool onTriggerValidate = false;
    bool canPlay = true;

    public void SetCanPlay(bool b)
    {
        canPlay = b;
        if (b == false)
        {
            rigidbody.velocity = Vector3.zero;
        }
        //rigidbody.simulated = b;
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
            //Validate();
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
        if(value.isPressed == true)
        {
            rotation -= rotateSpeed;
        }
        else
        {
            rotation += rotateSpeed;
        }
    }

    public void OnBumperRight(InputValue value)
    {
        if (value.isPressed == true)
        {
            rotation += rotateSpeed;
        }
        else
        {
            rotation -= rotateSpeed;
        }
    }

    public void Rotate()
    {
        this.transform.Rotate(new Vector3(0, 0, rotation * Time.deltaTime));
    }



    public void OnInteraction()
    {
        if(draggedPaper != null)
        {
            if (onTriggerValidate == true)
            {

            }
            else
            {
                draggedPaper.ReleaseObject(move);
                draggedPaper = null;
                return;
            }
        }
        int layerMask = 1 << 11;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2, layerMask) == true)
        {
            draggedPaper = hit.transform.GetComponent<MinigamePaperObject>();
            draggedPaper.HoldObject(this.transform);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interaction")
            onTriggerValidate = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interaction")
            onTriggerValidate = false;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Transform pivotCamera;
    [SerializeField]
    Camera camera;
    [SerializeField]
    CharacterController characterController;
    [SerializeField]
    float speed = 2;
    [SerializeField]
    float gravity = 2;
    [SerializeField]
    float sensitivity = 2;

    float speedX = 0;
    float speedZ = 0;
    float currentRotationX = 0;



    // Update is called once per frame
    void Update()
    {
        CheckPlayerInputCamera();
        Move();
    }

    public void Move()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical")) != 0)
        {
            speedZ = Input.GetAxis("Vertical");
        }
        else
        {
            speedZ = 0;
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) != 0)
        {
            speedX = Input.GetAxis("Horizontal");
        }
        else
        {
            speedX = 0;
        }

        var forward = pivotCamera.forward;
        var right = pivotCamera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        Vector3 move = right * speedX + forward * speedZ;
        move *= speed;

        //rigidbodyCharacter.velocity = move * defaultSpeed * Time.deltaTime;
        characterController.Move(move * Time.deltaTime);
        characterController.Move(new Vector3(0, gravity * Time.deltaTime, 0));
    }

    public void CheckPlayerInputCamera()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float rotateVertical = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        currentRotationX -= rotateVertical;
        currentRotationX = Mathf.Clamp(currentRotationX, -90f, 90f);

        pivotCamera.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
        transform.Rotate(Vector3.up * rotateHorizontal);
    }


}

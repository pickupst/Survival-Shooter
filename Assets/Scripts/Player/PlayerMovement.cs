using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6f;
    Animator animator;


    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    float rotationLerpCount = 4f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
    }


    private void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);

        Animating(h, v);

        Turning();

    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {

            Vector3 playerToMOuse = floorHit.point - transform.position;
            playerToMOuse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMOuse);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationLerpCount);
            //playerRigidbody.MoveRotation(newRotation);

        }
    }

    private void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0;
        animator.SetBool("isWalking", walking);
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement * Time.deltaTime * speed;
        playerRigidbody.MovePosition(transform.position + movement);
    }
}

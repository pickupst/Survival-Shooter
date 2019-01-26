using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JoystickManager joystickManager;

    public float speed = 6f;
    Animator animator;


    Vector3 movement;
    Rigidbody playerRigidbody;

    float rotationLerpCount = 4f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {

        float h = joystickManager.getJoystickVector().x;
        float v = joystickManager.getJoystickVector().z;
        Move(h, v);

        Animating(h, v);

        Turning();

    }

    private void Turning()
    {
        if (joystickManager.getJoystickVector() != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(joystickManager.getJoystickVector());

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationLerpCount);
            playerRigidbody.MoveRotation(newRotation);
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

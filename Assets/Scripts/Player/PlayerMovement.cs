using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6f;
    Vector3 movement;
    Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);

    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement * Time.deltaTime * speed;
        playerRigidbody.MovePosition(transform.position + movement);
    }
}

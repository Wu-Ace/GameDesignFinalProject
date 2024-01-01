using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Plane plane;
    private Vector3 moveDirection;
    private Vector3 movement;
    private Rigidbody rb;
    public float moveSpeed = 5f;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 moveX = Input.GetAxisRaw("Horizontal") * Vector3.right;
        Vector3 moveZ = Input.GetAxisRaw("Vertical") * Vector3.forward;
        transform.Translate(moveZ * moveSpeed * Time.deltaTime);
        transform.Translate(moveX * moveSpeed * Time.deltaTime);
      
    }
}
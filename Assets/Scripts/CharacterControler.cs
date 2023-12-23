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

    void Update()
    {
        Vector3 moveX = Input.GetAxisRaw("Horizontal") * Vector3.right;
        Vector3 moveZ = Input.GetAxisRaw("Vertical") * Vector3.forward;
        // moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        //
        // // 根据移动方向设置速度
        // movement = moveDirection * moveSpeed;
        // rb.velocity = new Vector3(movement.x, 0f, movement.z);
        transform.Translate(moveZ * moveSpeed * Time.deltaTime);
        transform.Translate(moveX * moveSpeed * Time.deltaTime);
        // Vector3 targetDirection = Vector3.back;
        // Vector3 currentDirection = transform.up;
        //
        // float rotationAngle = Mathf.Acos(Vector3.Dot(targetDirection.normalized, currentDirection.normalized)) * Mathf.Rad2Deg;//这行代码是用来计算两个向量之间的夹角的
        //
        // float lerpValue = 0.1f;//这个值越大，旋转越快
        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection, Vector3.up), lerpValue);//这行代码是用来旋转的，其原理是用四元数来表示旋转，然后用Lerp函数来插值，从而实现旋转
        // Vector3 newPosition = transform.position + transform.forward * Time.deltaTime * 10f;//这行代码是用来移动的，其原理是用当前物体的前方向量乘以时间乘以速度，从而实现移动
        // newPosition.z = 0;
        // if (plane.GetDistanceToPoint(newPosition) > 0)//plane.GetDistanceToPoint的意思是，计算一个点到平面的距离，如果这个距离大于0，说明这个点在平面的正面，如果小于0，说明这个点在平面的背面
        // {
        //     transform.position = newPosition;
        // }
    }
}
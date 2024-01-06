using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    public static float moveSpeed = 25f;
    public float jumpForce = 10000f;
    public float fallMultiplier = 2.5f;
    public static float obstacleSlowdownFactor = 0.2f;
    public static Rigidbody rb;
    private bool isGrounded = true;
    private int health = 100; // 玩家血量
    private bool canTakeDamage = true; // 是否能够受到伤害
    public float damageCooldown = 1f; // 伤害冷却时间
    public float deathTime = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            Debug.Log("jump");
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // 在此更新伤害冷却计时器
        if (!canTakeDamage)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0)
            {
                canTakeDamage = true;
                damageCooldown = 5f; // 重置冷却时间
            }
        }
    }

    void MoveCharacter()
    {
        Vector3 moveX = Input.GetAxisRaw("Horizontal") * Vector3.right;
        Vector3 moveZ = Input.GetAxisRaw("Vertical") * Vector3.forward;

        Vector3 moveDirection = (moveX + moveZ).normalized;
        moveDirection *= moveSpeed;

        transform.Translate(moveDirection * Time.deltaTime);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            TakeDamage(10); 
            // 碰到敌人扣除 10 点血量
            canTakeDamage = false; // 触发伤害冷却
        }
    }

    void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Player Health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        // 实现玩家死亡的逻辑
        GetComponent<CharacterControler>().enabled = false; // 停止移动脚本
        GetComponent<Rigidbody>().velocity = Vector3.zero; // 停止速度
        // 此处可添加播放死亡动画或其他动作
        Destroy(gameObject, deathTime);
        Debug.Log("Player died");
        // 可以在这里执行玩家死亡后的操作
    }
    void CheckIfGrounded(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
    }
    
}
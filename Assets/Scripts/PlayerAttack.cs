using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float moveSpeed = CharacterControler.moveSpeed;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        { 
            Rigidbody rb = GetComponent<Rigidbody>();
            float impactForce = rb.velocity.magnitude; // 获取碰撞力大小
            // Debug.Log(impactForce);
            int attackDamage = CalculateDamage(impactForce);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            // Debug.Log(attackDamage);

            moveSpeed = CharacterControler.moveSpeed * CharacterControler.obstacleSlowdownFactor;
            // Debug.Log("Hit");
        }
        else
        {
            moveSpeed = CharacterControler.moveSpeed;
        }
    }

    int CalculateDamage(float force)
    {
            // 根据力的大小计算伤害量
        if (force >= 5f) 
        { 
            return 10; // 较大的伤害值，例如10
        }
        else if (force >= 1f)
        {
            return 5; // 较小的伤害值，例如5
        }
        else
        {
            return 0; // 如果力太小则不造成伤害
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // 敌人最大血量
    private int currentHealth; // 当前血量

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 敌人死亡逻辑，例如停止移动、播放死亡动画等
        GetComponent<EnemyAi>().enabled = false; // 停止移动脚本
        GetComponent<Rigidbody>().velocity = Vector3.zero; // 停止速度
        // 此处可添加播放死亡动画或其他动作
        Destroy(gameObject, 2f); // 两秒后销毁敌人物体
    }
}

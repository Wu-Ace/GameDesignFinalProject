using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // 敌人最大血量
    private int currentHealth; // 当前血量
    public float deathTime = 0.2f;
    public AudioClip[] hitSounds; // 存储多个受击音效
    private AudioSource audioSource; // 用于播放音效的组件
    public GameObject hitEffectPrefab; // 受伤特效预制体
    public static int deadEnemyCount = 0;
    private static UIManager _uiManager;


    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>(); // 获取 AudioSource 组件
        if (_uiManager == null)
        {
            _uiManager = GameObject.FindObjectOfType<UIManager>();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            PlayRandomHitSound(); // 播放随机受击音效
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    void Die()
    {
        // 增加死去的敌人数量
        deadEnemyCount++;
        // 更新 UI 文本
        if (_uiManager != null)
        {
            _uiManager.UpdateDeadEnemyCountText();
        }
        // 敌人死亡逻辑，例如停止移动、播放死亡动画等
        GetComponent<EnemyAi>().enabled = false; // 停止移动脚本
        GetComponent<Rigidbody>().velocity = Vector3.zero; // 停止速度
        // 此处可添加播放死亡动画或其他动作
        Destroy(gameObject, deathTime); // 两秒后销毁敌人物体 
    }

    void PlayRandomHitSound()
    {
        if (hitSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            audioSource.clip = hitSounds[randomIndex];
            audioSource.Play();
        }
    }
}
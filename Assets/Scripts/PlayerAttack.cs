using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // public AudioClip[] hitSounds; // 存储多个打击音效
    // private AudioSource audioSource; // 用于播放音效的组件

    private float moveSpeed = CharacterControler.moveSpeed;

    private void Start()
    {
        // 获取 AudioSource 组件
        // audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        { 
            Rigidbody rb = GetComponent<Rigidbody>();
            float impactForce = rb.velocity.magnitude;
            int attackDamage = CalculateDamage(impactForce);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);

            moveSpeed = CharacterControler.moveSpeed * CharacterControler.obstacleSlowdownFactor;

            // 播放随机的打击音效
            // PlayRandomHitSound();
        }
        else
        {
            moveSpeed = CharacterControler.moveSpeed;
        }
    }

    int CalculateDamage(float force)
    {
        if (force >= 10f) 
        { 
            return 20;
        }
        else if (force >= 5f)
        {
            return 5;
        }
        else
        {
            return 0;
        }
    }

    // void PlayRandomHitSound()
    // {
    //     // 检查是否有音效可用
    //     if (hitSounds.Length > 0 && audioSource != null)
    //     {
    //         // 从数组中随机选择一个音效并播放
    //         int randomIndex = Random.Range(0, hitSounds.Length);
    //         audioSource.clip = hitSounds[randomIndex];
    //         audioSource.Play();
    //     }
    // }
}
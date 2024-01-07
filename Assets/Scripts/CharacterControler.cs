using System.Collections;
using UnityEngine;

using Cinemachine;

public class CharacterControler : MonoBehaviour
{
    public static float moveSpeed = 25f;
    public float jumpForce = 10000f;
    public float fallMultiplier = 2.5f;
    public static float obstacleSlowdownFactor = 0.2f;
    public static Rigidbody rb;
    private bool isGrounded = true;
    private int health = 100;
    private bool canTakeDamage = true;
    public float damageCooldown = 0.8f;
    public float deathTime = 1f;
    public CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin noise;
    public CinemachineImpulseSource impulseSource;
    public CinemachineVirtualCamera mainCamera;
    public AudioClip[] hurtSounds; // 存储多个受伤音效
    private AudioSource audioSource; // 用于播放音效的组件
    public GameObject Restart;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // impulseSource = GetComponent<CinemachineImpulseSource>();
        // // virtualCamera = GetComponent<CinemachineVirtualCamera>();
        // noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin();
        //
        // noise.m_AmplitudeGain = 0; //震动个数
        // noise.m_FrequencyGain = 0; //震动幅度
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

        if (!canTakeDamage)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0)
            {
                canTakeDamage = true;
                damageCooldown = 5f;
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

            // 落到地面上时震飞周围物体
            ShakeSurroundingObjects();
            impulseSource.GenerateImpulseAtPositionWithVelocity(transform.position, Vector3.up * 10f);
            mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1f;
            Invoke("ResetCameraShake", 1f);
            PlayRandomHurtSound();
        }
        else if (other.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            TakeDamage(10);
            canTakeDamage = false;
            mainCamera.m_Lens.FieldOfView -= 3f; 
            mainCamera.m_Lens.Dutch -= 5f;
            // PlayRandomHurtSound(); // 播放受伤音效

        }
        else if (other.gameObject.CompareTag("Border"))
        {
            Die();
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
        else
        {
        }
    }

    void Die()
    {
        GetComponent<CharacterControler>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        // Destroy(gameObject, deathTime);
        Restart.SetActive(true);
        Debug.Log("Player died");
    }

    void ShakeSurroundingObjects()
    {
        // 获取周围的物体，这里简化为获取所有Collider在半径为5的范围内的物体
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f);

        foreach (Collider collider in colliders)
        {
            Rigidbody otherRb = collider.GetComponent<Rigidbody>();

            // 需要给物体添加Rigidbody组件
            if (otherRb != null)
            {
                Vector3 direction = (otherRb.transform.position - transform.position).normalized;
                otherRb.AddForce(direction * 5f, ForceMode.Impulse);
            }
        }
    }
    void ResetCameraShake()
    {
        mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }
    void PlayRandomHurtSound()
    {
        if (hurtSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, hurtSounds.Length);
            audioSource.clip = hurtSounds[randomIndex];
            audioSource.Play();
        }
    }
}

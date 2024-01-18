using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private Transform headTransform;
    private Transform playerTransform;
    private float initialAngle;
    public float rotationThreshold = 360f;  // 这里可以根据需要调整，比如720f、1080f等
    public GameObject enemySpawner;
    private float lastResetTime;
    private Vector3 directionToPlayer;
    public GameObject playerUI;

    private bool isPlayerRotating = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        if (playerTransform = null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (headTransform = null)
        {
            headTransform = GameObject.FindGameObjectWithTag("Head").transform;

        }

    }
    
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 重新获取引用
        if (playerTransform != null)
            playerTransform = playerTransform.transform;
        else
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (headTransform != null)
            headTransform = headTransform;
        else
            headTransform = GameObject.FindGameObjectWithTag("Head").transform;
        // if (playerUI != null)
        // {
        //     playerUI.SetActive(false);
        // }
    }
    

    private void Start()
    {
        lastResetTime = Time.time; // 记录上一次初始角度重置的时间
    }

    private void Update()
    {
        // 当directionToPlayer.magnitude < 1 时进行检测
        if (playerTransform != null)
            playerTransform = playerTransform.transform;
        else
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (headTransform != null)
            headTransform = headTransform;
        else
            headTransform = GameObject.FindGameObjectWithTag("Head").transform;
        directionToPlayer = headTransform.position - playerTransform.position;
        float currentAngle = GetPlayerAngle();
        // Debug.Log(directionToPlayer);
        
        // if(playerUI == null)
        // {
        //     playerUI = GameObject.Find("PlayerUI");
        // }

        // 如果玩家开始转动
        if (!isPlayerRotating && directionToPlayer.y < 3)
        {
            // 记录初始角度
            initialAngle = currentAngle;
            isPlayerRotating = true;
        }

        if (isPlayerRotating)
        {
            // 计算当前角度与初始角度的差异
            float angleDifference = currentAngle - initialAngle;
            Debug.Log(angleDifference);
            
            // 检查是否完成了一整圈旋转
            if (angleDifference >= rotationThreshold || angleDifference <= -rotationThreshold)
            {
                // 激活敌人生成器
                enemySpawner.gameObject.SetActive(true);
                if (playerUI != null)
                {
                    playerUI.SetActive(false);

                }
                    // 为下一次检查更新初始角度
                initialAngle = currentAngle;
                isPlayerRotating = false; // 头部完成一整圈旋转后重置状态
            }
        }

        // 每隔0.1秒重置一次初始角度
        if (Time.time - lastResetTime >= 0.1f)
        {
            isPlayerRotating = false; // 重置状态
            lastResetTime = Time.time;
        }
    }

    float GetPlayerAngle()
    {
        // 计算相对于玩家位置的角度
        float angle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        return angle;
    }
}

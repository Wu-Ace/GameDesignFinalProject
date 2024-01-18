using System.Collections;
using UnityEngine;

public class ChargerAI : MonoBehaviour
{
    private State state;
    private Transform playerTransform;
    public float enemyMoveSpeed = 8.0f;
    public float dashSpeed = 15.0f;
    public float dashDuration = 1.0f;
    public float timeBetweenDashes = 5.0f;

    private enum State
    {
        Roaming,
        ChaseTarget,
        ShootingTarget,
        GoingBackToStart,
        Dashing,
    }

    private void Awake()
    {
        state = State.Roaming;
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;

        // 设置初始状态为追逐目标
        state = State.ChaseTarget;

        StartCoroutine(DashPeriodically());
    }

    void Update()
    {
        switch (state)
        {
            case State.Roaming:
                // 在这里添加漫游逻辑
                break;
            case State.ChaseTarget:
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, enemyMoveSpeed * Time.deltaTime);
                break;
            case State.ShootingTarget:
                // 在这里添加向目标射击逻辑
                break;
            case State.GoingBackToStart:
                // 在这里添加返回起始点逻辑
                break;
            case State.Dashing:
                // 在DashPeriodically协程中处理冲刺逻辑
                break;
        }
    }

    IEnumerator DashPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenDashes);

            // 切换状态到冲刺状态
            state = State.Dashing;

            // 冲刺向玩家
            Vector3 dashDirection = (playerTransform.position - transform.position).normalized;
            float dashEndTime = Time.time + dashDuration;

            while (Time.time < dashEndTime)
            {
                transform.position += dashDirection * dashSpeed * Time.deltaTime;
                yield return null;
            }

            // 恢复状态为追逐目标
            state = State.ChaseTarget;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    // Start is called before the first frame update
    private State state;
    private Transform playerTransform;
    public float enemyMoveSpeed = 8.0f;
    private enum State {
        Roaming,
        ChaseTarget,
        ShootingTarget,
        GoingBackToStart,
    }

    private void Awake()
    {
        state = State.Roaming;
    }

    void Start()
    {
        //player是带有“player"tag的物体
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.transform.position, enemyMoveSpeed * Time.deltaTime);
    }
}

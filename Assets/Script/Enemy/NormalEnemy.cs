﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField] private Rigidbody2D enemyRD;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.enemyType = EnemyType.Normal;
        this.ATK = 1;
        this.HP = 5;
        this.speed = 2f;
        this.detectRange = 5f;
        this.attackRange = 1f;
        this.cooldownTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isDetect)
        {
            DetectPlayer(player);
        }

        switch (this.enemyState)
        {
            case EnemyState.Idle:
                Debug.Log("Enemy Idle");
                break;
            case EnemyState.MoveToPosition:
                Debug.Log("Enemy Moving to Position");
                break;
            case EnemyState.Detected:
                Debug.Log("Enemy Detected");
                this.enemyState = EnemyState.MoveToPlayer;
                break;
            case EnemyState.MoveToPlayer:
                Debug.Log("Enemy Moving to Player");
                MoveToPlayer(this.player);
                break;
            case EnemyState.Cooldown:
                Debug.Log("Enemy Cooldowing");
                StartCoroutine(CooldowingTime(this.cooldownTime));
                this.enemyState = EnemyState.Preparing;
                break;
            case EnemyState.Preparing:
                this.enemyState = this.ValidatePlayerInEnemyRange(this.player) ? EnemyState.EnemyHit : EnemyState.Preparing;
                break;
            case EnemyState.EnemyHit:
                Debug.Log("Enemy Attack");
                AttackPlayer(this.player);
                this.enemyState = EnemyState.Cooldown;
                break;
            case EnemyState.PlayerHit:
                Debug.Log("Enemy Attacked");
                break;
        }
    }

    public void MoveToPlayer(GameObject player)
    {
        float distance = Vector2.Distance(this.transform.position, player.transform.position);
        Debug.Log(distance);
        if (distance > 1)
        {
            Vector3 direction = (player.transform.position - this.transform.position).normalized;
            this.transform.position += this.speed * Time.deltaTime * direction;
        }
        else
        {
            this.enemyState = EnemyState.EnemyHit;
        }
    }

    public void AttackPlayer(GameObject player)
    {
        PlayerHealth playerHealthy = player.GetComponent<PlayerHealth>();
        if (playerHealthy.HP > 0)
        {
            playerHealthy.DecreaseHealth(this.ATK);
        }
    }
}

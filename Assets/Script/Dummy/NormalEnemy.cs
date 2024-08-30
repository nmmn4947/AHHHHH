using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField] private Rigidbody2D enemyRD;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.enemyType = EnemyType.Normal;
        this.ATK = 5;
        this.HP = 5;
        this.speed = 5f;
        this.detectRange = 10f;
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
                MoveToPlayer();
                break;
            case EnemyState.Cooldown:
                this.enemyState = this.ValidateEnemyInPlayerRange(this.player) ? EnemyState.EnemyHit : EnemyState.Idle;
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

    public void MoveToPlayer()
    {
        // If enemy stay in player range change state enemy
        this.enemyState = EnemyState.EnemyHit;
    }

    public void AttackPlayer(GameObject player)
    {
        PlayerHealth playerHealthy = player.GetComponent<PlayerHealth>();
        if(playerHealthy.HP > 0)
        {
            playerHealthy.DecreaseHealth(this.ATK);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteEnemy : Enemy
{
    [SerializeField] private Rigidbody2D enemyRD;
    [SerializeField] private GameObject player;
    public float biteRange;
    public float chargingTime;
    public bool isCharg = false;

    // Start is called before the first frame update
    void Start()
    {
        this.ATK = 10;
        this.HP = 20;
        this.speed = 2f;
        this.detectRange = 5f;
        this.biteRange = 5f;
        this.cooldownTime = 10f;
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
                this.enemyState = EnemyState.Charging;
                break;
            case EnemyState.Charging:
                Debug.Log("Enemy Charging");
                ChargBite();
                break;
            case EnemyState.MoveToPlayer:
                Debug.Log("Enemy Moving to Player");
                break;
            case EnemyState.Cooldown:
                this.enemyState = this.ValidatePlayerInEnemyRangeATK(this.player) ? EnemyState.EnemyHit : EnemyState.Idle;
                break;
            case EnemyState.EnemyHit:
                Debug.Log("Enemy Attack");
                AttackPlayer();
                this.enemyState = EnemyState.Cooldown;
                break;
            case EnemyState.PlayerHit:
                Debug.Log("Enemy Attacked");
                break;
        }
    }

    private void ChargBite()
    {
        // Stay idle 5 sec until then complete charging
        // isCharg = true
        this.enemyState = EnemyState.MoveToPlayer;
    }

    public void MoveToPlayer()
    {
        // Change speed of enemy to 10 unit
        // If enemy stay in player range change state enemy
        this.enemyState = EnemyState.EnemyHit;
    }

    public void AttackPlayer()
    {
        PlayerHealth playerHealthy = player.GetComponent<PlayerHealth>();
        if (playerHealthy.HP > 0)
        {
            playerHealthy.DecreaseHealth(this.ATK);
        }
    }
}

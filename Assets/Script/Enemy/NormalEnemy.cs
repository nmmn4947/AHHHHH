using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField] private Rigidbody2D enemyRD;
    [SerializeField] private GameObject player;

    public Rigidbody2D EnemyRD { get => enemyRD; set => enemyRD = value; }
    public GameObject Player { get => player; set => player = value; }

    // Start is called before the first frame update
    void Start()
    {
        this.ATK = 1;
        this.HP = 5;
        this.speed = 2f;
        this.detectRange = 5f;
        this.attackRange = 1f;
        this.cooldownTime = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        /*
        if (!this.isCooldown)
        {
            if (!this.isDetect)
            {
                DetectPlayer(Player);
            }
            else
            {
                IsPlayerInRangeDetect(this.Player);
            }
        }
        */

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
                MoveToPlayer(this.Player);
                break;
            case EnemyState.Cooldown:
                Debug.Log("Enemy Cooldowing");
                Destroy(this.gameObject);
                //StartCoroutine(CooldowingTime(this.cooldownTime));
                this.enemyState = EnemyState.Preparing;
                break;
            case EnemyState.Preparing:
                this.enemyState = this.IsPlayerInRangeATK(this.Player) ? EnemyState.EnemyHit : EnemyState.Preparing;
                break;
            case EnemyState.EnemyHit:
                Debug.Log("Enemy Attack");
                AttackPlayer(this.Player);
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
            /*
            if (distance > this.detectRange)
            {
                this.enemyState = EnemyState.Idle;
            }
            else
            {
                Vector3 direction = (player.transform.position - this.transform.position).normalized;
                this.transform.position += this.speed * Time.deltaTime * direction;
            }
            */
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

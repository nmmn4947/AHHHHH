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
        this.ATK = 5;
        this.HP = 5;
        this.speed = 5;
        this.detectRange = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer(player);
        /*
        DetectPlayer();
        switch (this.enemyState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Detected:
                break;
            case EnemyState.MoveTo:
                break;
            case EnemyState.Cooldown:
                break;
            case EnemyState.EnemyHit:
                break;
            case EnemyState.PlayerHit: 
                break;
        }
        */
    }

    public void MoveToPlayer()
    {
        // 
    }

    public void AttackPlayer(GameObject player)
    {
        
    }
}

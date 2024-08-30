using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    public float bombRange;
    // Start is called before the first frame update
    void Start()
    {
        this.enemyType = EnemyType.Bomb;
        this.ATK = 5;
        this.HP = 5;
        this.speed = 2f;
        this.detectRange = 10f;
        this.bombRange = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

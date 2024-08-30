using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;
    public EnemyState enemyState;
    public float speed;
    public float attackRange;
    public float detectRange;
    public float moveRange;
    public bool isDetect = false;
    public int HP;
    public int ATK;
    public float cooldownTime;
    public bool isCooldown = false;

    [SerializeField] private int maxHP;
    private int minHP;

    public void DecreaseHP(int damage)
    {
        this.HP -= damage;
        if (this.HP == minHP)
        {
            Die();
            return;
        }
    }

    public void IncreaseHP(int heart)
    {
        this.HP += heart;
        if (this.HP >= maxHP)
        {
            this.HP = this.maxHP;
            return;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyState = EnemyState.PlayerHit;
        DecreaseHP(damage);
    }

    public void Heal(int amount)
    {
        IncreaseHP(amount);
    }

    private void Die()
    {
        //Die
        Destroy(this.gameObject);
    }

    public void DetectPlayer(GameObject player)
    {
        // Find solution for detect player
        this.isDetect = true;
        // If detected player
        if (this.isDetect)
        {
            enemyState = EnemyState.Detected;
        }
    }

    public bool ValidateEnemyInPlayerRange(GameObject player)
    {
        return false;
        // If enemy stay in player range change state enemy
        //return true;
        // If enemy not stay in player range change state enemy
        //return false;
    }

    public void EnemyMoveToLeft()
    {
        //Move to left side + 100 unit
        //Stay idle 10 sec
        //Move to center
    }

    public void EnemyMoveToRight()
    {
        //Move to right side + 100 unit
        //Stay idle 10 sec
        //Move to center
    }

    public void EnemyIdle()
    {
        //Stay idle 10 sec
        //Radom Left or Right first only 1 time?
        //change state enemy
    }

    public enum EnemyType
    {
        Normal,
        Flying,
        Bite
    }

    public enum EnemyState
    {
        Idle,
        Detected,
        Charging,
        MoveToPosition,
        MoveToPlayer,
        PlayerHit, // Mean player attack enemy
        EnemyHit, // Mean enemy attack player
        Cooldown,
        Stun,
    }
}

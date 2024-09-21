using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        float distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (distance <= this.detectRange)
        {
            this.isDetect = true;
            enemyState = EnemyState.Detected;
        }
    }

    public bool ValidatePlayerInEnemyRange(GameObject player)
    {
        float distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (distance > this.detectRange)
        {
            this.isDetect = false;
            return false;
        }
        else
        {
            if(isCooldown)
            {
                return false;
            }
            return true;
        }
    }

    public IEnumerator CooldowingTime(float duration)
    {
        Debug.Log("Strat Cooldown Time");
        this.isCooldown = true;
        //this.enemyState = EnemyState.Preparing;
        yield return new WaitForSeconds(duration);
        this.isCooldown = false;
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
        Preparing, // Use for between cooldowing
        PlayerHit, // Mean player attack enemy
        EnemyHit, // Mean enemy attack player
        Cooldown,
        Stun,
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;
    public EnemyState enemyState;
    public int speed;
    public float attackRange;
    public float detectRange;
    public int HP;
    public int ATK;

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
        DecreaseHP(damage);
    }

    public void Heal(int amount)
    {
        IncreaseHP(amount);
    }

    public int Attack(GameObject player)
    {
        //enemy attack player
        return this.ATK;
    }

    private void Die()
    {
        //Die
        Destroy(this.gameObject);
    }

    public void DetectPlayer(GameObject player)
    {
        //Debug.Log("Detected Player");
        //See the player
        //if enemy detected player
        //enemyState = EnemyState.Detected;
    }

    public enum EnemyType
    {
        Normal,
        Flying,
        Bomb
    }

    public enum EnemyState
    {
        Idle,
        Detected,
        MoveTo,
        PlayerHit, // Mean player attack enemy
        EnemyHit, // Mean enemy attack player
        Cooldown
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class DummyTarget : MonoBehaviour
{
    [SerializeField] private int HP;
    private bool isDead;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void SetHP(int num)
    {
        HP = num;
    }

    public int getHp()
    {
        return HP;
    }

    public void IsHitting(Vector2 knock) 
    {
        rb.AddForce(knock);
    }

/*    private void KnockBack(Vector2 dir, Vector2 power)
    {
        rb.AddForce(dir * power);
    }*/

    private void TakeDamage(int damage)
    {
        HP -= damage;
    }

}

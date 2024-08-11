using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class DummyTarget : MonoBehaviour
{
    [SerializeField] private int HP;
    private bool isDead;
    private bool isHit;
    Rigidbody2D rb;

    private void Start()
    {
        isHit = false;
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
        isHit = true;
        rb.AddForce(knock);
    }

/*    private void KnockBack(Vector2 knock)
    {
        rb.AddForce(knock);
    }*/

    private void TakeDamage(int damage)
    {
        HP -= damage;
    }

}

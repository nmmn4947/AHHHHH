using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [SerializeField] private int HP;
    private bool isHit;
    private bool isDead;

    public void SetHP(int damage)
    {
        HP -= damage;
    }

    public int getHp()
    {
        return HP;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private int MaxHP = 3;
    [SerializeField] private int MinHP = 0;
    [SerializeField] private GameObject health;
    [SerializeField] private float immortalTime;

    private void Start()
    {
        HP = MaxHP;
    }

    public void IncreaseHealth()
    {
        this.HP++;
        if(this.HP >= MaxHP)
        {
            this.HP = MaxHP;
        }
        this.health.transform.GetChild(this.HP - 1).gameObject.SetActive(true);
    }

    public void DecreaseHealth()
    {
        this.HP--;
        if(this.HP <= MinHP)
        {
            this.HP = MinHP;
        }
        this.health.transform.GetChild(this.HP).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseHealth();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DecreaseHealth();
        }
    }
}

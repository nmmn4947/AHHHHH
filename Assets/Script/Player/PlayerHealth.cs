using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private int MaxHP = 3;
    [SerializeField] private int MinHP = 0;
    [SerializeField] private GameObject health;
    [SerializeField] private float immortalTime;
    public bool isImmortal = false;

    private void Start()
    {
        HP = MaxHP;
        immortalTime = 2f;
    }

    public void IncreaseHealth()
    {
        if (!isImmortal)
        {
            this.HP++;
            if (this.HP >= MaxHP)
            {
                this.HP = MaxHP;
            }
            this.health.transform.GetChild(this.HP - 1).gameObject.SetActive(true);
            StartCoroutine(ImmortalPlayer(immortalTime));
        }
    }

    public void DecreaseHealth()
    {
        if (!isImmortal)
        {
            this.HP--;
            if (this.HP <= MinHP)
            {
                this.HP = MinHP;
            }
            this.health.transform.GetChild(this.HP).gameObject.SetActive(false);
            StartCoroutine(ImmortalPlayer(immortalTime));
        }
    }

    IEnumerator ImmortalPlayer(float duration)
    {
        this.isImmortal = true;
        yield return new WaitForSeconds(duration);
        this.isImmortal = false;
    }
}
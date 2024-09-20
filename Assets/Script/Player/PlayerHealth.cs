using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int HP;
    [SerializeField] private int MAX_HP = 3;
    [SerializeField] private int MIN_HP = 0;
    [SerializeField] private GameObject health;
    [SerializeField] private float immortalTime;
    public bool isImmortal = false;

    private void Start()
    {
        HP = MAX_HP;
    }

    public void IncreaseHealth()
    {
        if (!isImmortal)
        {
            this.HP++;
            if (this.HP >= MAX_HP)
            {
                this.HP = MAX_HP;
            }
            this.health.transform.GetChild(this.HP - 1).gameObject.SetActive(true);
            StartCoroutine(ImmortalPlayer(immortalTime));
        }
    }

    public void DecreaseHealth(int damage)
    {
        if (!isImmortal)
        {
            this.HP -= damage;
            if (this.HP <= MIN_HP)
            {
                this.HP = MIN_HP;
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
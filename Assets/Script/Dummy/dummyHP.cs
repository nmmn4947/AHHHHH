using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyHP : MonoBehaviour
{
    public int HP;
    public bool isDead;
    public bool isHit;

    public void SetHP(int damage){
        HP -= damage;
        isHit = false;

    }
    public void isDying();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0){
            isDead();
        }
        if (isHit = true){
            SetHP();
        }
    }
}

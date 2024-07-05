using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimThing : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void setParameter(string name, bool set)
    {
        animator.SetBool(name, set);
    }
}

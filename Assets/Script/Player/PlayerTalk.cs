using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalk : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LoudThresh.isScreaming)
        {
            animator.SetBool("IsScream", true);
        }
        else if (LoudThresh.isTalking)
        {
            animator.SetBool("IsScream", false);
            animator.SetBool("IsTalk", true);
        }
        else
        {
            animator.SetBool("IsScream", false);
            animator.SetBool("IsTalk", false);
        }
    }
}

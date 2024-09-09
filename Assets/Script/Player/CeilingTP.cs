using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingTP : MonoBehaviour
{
    private float cooldown = 0.5f;
    private float keep = 0;
    private bool hit = false;
    private void Update()
    {
        if (hit)
        {
            keep += Time.deltaTime;
            if (keep > cooldown)
            {
                hit = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit) {
            if (collision.tag == "Ground")
            {
                gameObject.transform.parent.position = new Vector2(gameObject.transform.parent.position.x, collision.transform.position.y + 1);
                keep = 0f;
                hit = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private float SoundCharge;
    private int damage;
    private float Scale;
    private float hitStopTime;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // add up charge from sound
            // moreCharge = More damage, more Scale, more hitStopTime, more Range
            // until Max
        }
    }
}

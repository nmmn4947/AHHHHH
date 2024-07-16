using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private int SoundCharge = 0;
    [SerializeField] private int chargePerScream;
    [SerializeField] private int chargePerTalk;
    [SerializeField] private int MaxCharge;

    [SerializeField] private int damagePerCharge;
    [SerializeField] private float scalePerCharge;
    [SerializeField] private float hitStopTime;
    [SerializeField] private float hitRangePerCharge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Charge");
            
            if (SoundCharge <= MaxCharge)
            {
                if (LoudThresh.isTalking)
                {
                    SoundCharge += chargePerTalk;
                    Debug.Log("addingTal" + SoundCharge);
                }
                else
                {

                }
            }
            else
            {
                //max
            }

            // moreCharge = More damage, more Scale, more hitStopTime, more Range
            // until Max
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("punch!");
            //punch
            SoundCharge = 0;
        }
        else
        {
            Debug.Log("noting");
        }
    }
}

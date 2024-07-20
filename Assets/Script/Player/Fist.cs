using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    //charge
    [SerializeField] private float chargeTime;
    private float chargeTimeKeep;
    // damage
    [SerializeField] private int damagePerCharge;
    [SerializeField] private float MaxScaleCharge;
    // hit stop
    [SerializeField] private float hitStopTime;
    [SerializeField] private float hitRangePerCharge;
    //shaker
    Vector2 KeepFistPos;

    public GameObject skin;

    // Start is called before the first frame update
    void Start()
    {
        KeepFistPos = skin.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Charge");
            
            if (SoundSetting.isTalking)
            {
                /*SoundCharge += chargePerTalk;
                Debug.Log("addingTal" + SoundCharge);*/
                if (chargeTime > chargeTimeKeep)
                {
                    chargeTimeKeep += Time.deltaTime;
                }
            }
            else
            {

            }
            float min = Mathf.Lerp(-0.01f, -0.1f, chargeTimeKeep / chargeTime);
            float max = Mathf.Lerp(0.01f, 0.1f, chargeTimeKeep / chargeTime);
            skin.transform.localPosition = new Vector2(KeepFistPos.x + Random.Range(min, max), KeepFistPos.y + Random.Range(min, max));
            // moreCharge = More damage, more Scale, more hitStopTime, more Range
            // until Max
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("punch!");
            //punch
            
        }
        else
        {
            Debug.Log("noting");
        }


        Vector3 ones = new Vector3(1.0f, 1.0f, 1.0f) * Mathf.Lerp(1.0f, MaxScaleCharge, chargeTimeKeep / chargeTime);
        Debug.Log(ones);
        transform.localScale = ones;



        // do the Shake

    }
}

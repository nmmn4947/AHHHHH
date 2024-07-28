using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    
    public enum Phases{
        charging,
        releasing,
        hitting,
        returning
    }
    [Header("General")]
    public Phases currPhase;
    public GameObject skin;

    [Header("ChargePhase")]
    [SerializeField] private float chargeTime;
    private float chargeTimeKeep = 0;
    [SerializeField] private float MaxScaleCharge;
    Vector2 KeepFistSkinPos;

    [Header("ReleasePhase")]
    //setBackFirst then set Forward if set back is far enough
    [SerializeField] private float backReleaseTime;
    private float releaseTimeKeep = 0;
    private float releaseTimeScaling;
    [SerializeField] private float setBackRate;
    [SerializeField] private float forwardRate;
    [SerializeField] private float forwardReleaseTime;
    private float FReleaseTimeKeep;
    private float BkeepX;
    Vector2 KeepFistPos;

    [Header("HittingPhase")]
    // damage
    [SerializeField] private int damagePerCharge;
    // hit stop
    [SerializeField] private float hitStopTime;
    [SerializeField] private float hitRangePerCharge;

    // Start is called before the first frame update
    void Start()
    {
        KeepFistSkinPos = skin.transform.localPosition;
        KeepFistPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currPhase) {
            case Phases.charging:
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
                    skin.transform.localPosition = new Vector2(KeepFistSkinPos.x + Random.Range(min, max), KeepFistSkinPos.y + Random.Range(min, max));


                    // moreCharge = More damage, more Scale, more hitStopTime, more Range
                    // until Max
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    currPhase = Phases.releasing;
                    break;
                }
                else
                {
                    Debug.Log("noting");
                }

                Vector3 ones = new Vector3(1.0f, 1.0f, 1.0f) * Mathf.Lerp(1.0f, MaxScaleCharge, chargeTimeKeep / chargeTime);
                Debug.Log(ones);
                transform.localScale = ones;
                break;
            case Phases.releasing:
                releaseTimeScaling = Mathf.Lerp(0.0f, backReleaseTime, chargeTimeKeep / chargeTime);
                if (transform.localScale.x < 1.2f)
                {
                    currPhase = Phases.hitting; 
                    break;
                }
                else
                {
                    releaseTimeKeep += Time.deltaTime;
                    if (releaseTimeKeep < releaseTimeScaling)
                    {
                        Debug.Log("Blyat");
                        BkeepX = KeepFistSkinPos.x - Mathf.Lerp(0.0f, setBackRate, releaseTimeKeep / releaseTimeScaling);
                        skin.transform.localPosition = new Vector2(BkeepX, KeepFistSkinPos.y);
                    }
                    else
                    {
                        FReleaseTimeKeep += Time.deltaTime;
                        if (FReleaseTimeKeep < forwardReleaseTime) {
                            Debug.Log("Zuka");
                            skin.transform.localPosition = new Vector2(Mathf.Lerp(BkeepX, KeepFistSkinPos.x + forwardRate, FReleaseTimeKeep / forwardReleaseTime), KeepFistSkinPos.y);
                            
                        }
                        else
                        {
                            currPhase = Phases.hitting;
                            break;
                        }
                    }

                }
                break; 
            case Phases.hitting:
                //if hit


                chargeTimeKeep = 0;
                //currPhase = Phases.returning;
                break;
            case Phases.returning:
                currPhase = Phases.charging;
                break;
        }




        // do the Shake

    }
}

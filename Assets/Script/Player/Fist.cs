using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    
    public enum Phases{
        charging,
        releasing,
        hitting,
        returning,
        reset
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
    private bool isHitEnemy;

    [SerializeField] private float hitWindowTime;
    private float hitTimeKeep;
    // damage
    [SerializeField] private int damagePerCharge;
    // hit stop
    [SerializeField] private float hitStopTime;
    [SerializeField] private float hitRangePerCharge;

    [Header("ReturnPhase")]
    [SerializeField] private float returnTime;
    private float returnTimeKeep;

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
                    FReleaseTimeKeep += Time.deltaTime;
                    if (FReleaseTimeKeep < forwardReleaseTime)
                    {
                        skin.transform.localPosition = new Vector2(Mathf.Lerp(KeepFistSkinPos.x, KeepFistSkinPos.x + forwardRate, FReleaseTimeKeep / forwardReleaseTime), KeepFistSkinPos.y);
                    }
                    else
                    {
                        currPhase = Phases.hitting;
                        break;
                    }
                    currPhase = Phases.hitting; 
                    break;
                }
                else
                {
                    releaseTimeKeep += Time.deltaTime;
                    if (releaseTimeKeep < releaseTimeScaling)
                    {
                        BkeepX = KeepFistSkinPos.x - Mathf.Lerp(0.0f, setBackRate, releaseTimeKeep / releaseTimeScaling);
                        skin.transform.localPosition = new Vector2(BkeepX, KeepFistSkinPos.y);
                    }
                    else
                    {
                        FReleaseTimeKeep += Time.deltaTime;
                        if (FReleaseTimeKeep < forwardReleaseTime) {
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
                Debug.Log("Hitting");
                if (!isHitEnemy) { 
                    hitTimeKeep += Time.deltaTime;
                    if (hitTimeKeep < hitWindowTime)
                    {

                    }
                    else
                    {
                        currPhase = Phases.returning;
                        break;
                    }
                }
                else
                {
                    Debug.Log("Hit");
                    StartCoroutine(hitStop(hitStopTime));
                    currPhase = Phases.returning;
                    break;
                }
                break;
            case Phases.returning:
                returnTimeKeep += Time.deltaTime;
                if (returnTimeKeep < returnTime)
                {
                    transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1.0f, returnTimeKeep / returnTime), Mathf.Lerp(transform.localScale.y, 1.0f, returnTimeKeep / returnTime), 1.0f);
                    skin.transform.localPosition = new Vector2(Mathf.Lerp(skin.transform.localPosition.x, KeepFistSkinPos.x, returnTimeKeep / returnTime),
                                                               Mathf.Lerp(skin.transform.localPosition.y, KeepFistSkinPos.y, returnTimeKeep / returnTime));
                }
                else
                {
                    currPhase = Phases.reset;
                    break;
                }
                break;
            case Phases.reset:
                chargeTimeKeep = 0.0f;
                releaseTimeKeep = 0.0f;
                FReleaseTimeKeep = 0.0f;
                returnTimeKeep = 0.0f;
                currPhase = Phases.charging;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currPhase == Phases.hitting)
        {
            Debug.Log("YES");
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Sir");
                isHitEnemy = true;
            }
            else
            {
                Debug.Log("NOPE");
            }
        }
        else
        {
            Debug.Log("No");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currPhase == Phases.hitting)
        {
            Debug.Log("YES");
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Sir");
                isHitEnemy = true;
            }
            else
            {
                Debug.Log("NOPE");
            }
        }
        else
        {
            Debug.Log("No");
        }
    }

    IEnumerator hitStop(float duration)
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
    }
}

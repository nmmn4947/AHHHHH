using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;
using static Unity.Collections.AllocatorManager;

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
    SpriteRenderer spriteRenderer;

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
    float offsetC;
    BoxCollider2D boxC;

    [SerializeField] private float hitWindowTime;
    private float hitTimeKeep;
    // damage
    [SerializeField] private int damagePerCharge;
    // hit stop
    [SerializeField] private float hitStopTime;
    [SerializeField] private float maxShakeIntensity;
    private float shakeIntensity;
    [SerializeField] private float maxKnockPower;
    CinemachineBrain camBrain;

    [Header("ReturnPhase")]
    [SerializeField] private float returnTime;
    private float returnTimeKeep;

    // Start is called before the first frame update
    void Start()
    {
        KeepFistSkinPos = skin.transform.localPosition;
        KeepFistPos = transform.localPosition;
        spriteRenderer = skin.GetComponent<SpriteRenderer>();
        boxC = GetComponent<BoxCollider2D>();
        camBrain = FindAnyObjectByType<CinemachineBrain>();
        camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
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
                    if (chargeTimeKeep > chargeTime)
                    {
                        CamManager.Instance.SetShakeIntensity(0.0f);
                    }
                    else
                    {
                        CamManager.Instance.SetShakeIntensity(Mathf.Lerp(0.0f, 0.75f, chargeTimeKeep / chargeTime));
                    }
                    // moreCharge = More damage, more Scale, more hitStopTime, more Range
                    // until Max
                    shakeIntensity = Mathf.Lerp(0.0f,maxShakeIntensity, chargeTimeKeep / chargeTime);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    CamManager.Instance.SetShakeIntensity(0.0f);
                    currPhase = Phases.releasing;
                    break;
                }
                else
                {

                }

                Vector3 ones = new Vector3(1.0f, 1.0f, 1.0f) * Mathf.Lerp(1.0f, MaxScaleCharge, chargeTimeKeep / chargeTime);
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
                            offsetC = Mathf.Lerp(0 ,forwardRate, FReleaseTimeKeep / forwardReleaseTime);
                        }
                        else
                        {
                            camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
                            currPhase = Phases.hitting;
                            break;
                        }
                    }

                }
                break;
            case Phases.hitting:
                //if hit
                boxC.offset = new Vector2(0.6f + offsetC, boxC.offset.y);
                if (!isHitEnemy) { 
                    hitTimeKeep += Time.deltaTime;
                    if (hitTimeKeep < hitWindowTime)
                    {
                        spriteRenderer.color = Color.black;
                    }
                    else
                    {
                        spriteRenderer.color = Color.white;
                        currPhase = Phases.returning;
                        break;
                    }
                }
                else
                {
                    spriteRenderer.color = Color.red;
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
                shakeIntensity = 0.0f;
                //
                releaseTimeKeep = 0.0f;
                FReleaseTimeKeep = 0.0f;
                //
                hitTimeKeep = 0.0f;
                boxC.offset = new Vector2(0.6f, boxC.offset.y);
                offsetC = 0.0f;
                camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
                //
                returnTimeKeep = 0.0f;
                currPhase = Phases.charging;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DummyTarget target = collision.gameObject.GetComponent<DummyTarget>();
            Vector2 diff = target.gameObject.transform.position - new Vector3(KeepFistPos.x, KeepFistPos.y, 0);
            float dist = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y);
            if (dist > 0.0f)
            {
                diff /= dist;
            }
            Vector2 force = diff * maxKnockPower;
            if (currPhase == Phases.hitting)
            {
                target.IsHitting(force);
                isHitEnemy = true;
            }else
            {
                //target.IsHitting(diff * 2000.0f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DummyTarget target = collision.gameObject.GetComponent<DummyTarget>();
            
            Vector2 diff = target.gameObject.transform.position - new Vector3(KeepFistPos.x, KeepFistPos.y - 3, 0);
            float dist = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y);
            if (dist > 0.0f)
            {
                diff /= dist;
            }
            Vector2 force = diff * maxKnockPower;
            if (currPhase == Phases.hitting)
            {
                target.IsHitting(force);
                isHitEnemy = true;
            }
            else
            {
                //target.IsHitting(diff * 2000.0f);
            }
        }
    }

    IEnumerator hitStop(float duration)
    {
        Time.timeScale = 0.0f;
        Debug.Log("Shake intens = " + shakeIntensity.ToString());
        CamManager.Instance.SetShakeIntensity(shakeIntensity);
        yield return new WaitForSecondsRealtime(duration);
        CamManager.Instance.SetShakeIntensity(0.0f);
        spriteRenderer.color = Color.white;
        Time.timeScale = 1.0f;
    }
}

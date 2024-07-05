using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLoud : MonoBehaviour
{
    public bool GoUp;
    public float MaxDistance;
    private Vector3 OriginPos;
    private Vector3 MaxPos;

    private void Start()
    {
        OriginPos = this.transform.position;
    }

    private void Update()
    {
        if (GoUp)
        {
            MaxPos = new Vector3 (OriginPos.x, OriginPos.y + MaxDistance, OriginPos.z);
            transform.position = Vector3.Lerp(OriginPos, MaxPos, MicInput.Loudness);
        }
        else
        {
            MaxPos = new Vector3(OriginPos.x, OriginPos.y - MaxDistance, OriginPos.z);
            transform.position = Vector3.Lerp(OriginPos, MaxPos, MicInput.Loudness);
        }
    }
}
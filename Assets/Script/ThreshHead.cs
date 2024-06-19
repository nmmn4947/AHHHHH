using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreshHead : MonoBehaviour
{
    public Vector3 NormalPos;
    public Vector3 ScreamPos;
    private Vector3 OriginPos;
    // Start is called before the first frame update
    void Start()
    {
        OriginPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (MicInput.Loudness > LoudThresh.screamLoud)
        {
            transform.position = OriginPos + NormalPos;
        }
        else if (MicInput.Loudness > LoudThresh.normalLoud)
        {
            transform.position = OriginPos + ScreamPos;
        }
        else
        {
            transform.position = OriginPos;
        }
    }
}

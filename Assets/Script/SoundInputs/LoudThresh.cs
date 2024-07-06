using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoudThresh : SoundSetting
{
    public static bool isTalking;
    public static bool isScreaming;
    public bool isNormal;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        normalLoud = 0.0f;
        screamLoud = 0.0f;
    }

    private void Update()
    {
        if (MicInput.Loudness > LoudThresh.screamLoud)
        {
            isScreaming = true;
            

        }
        else if (MicInput.Loudness > LoudThresh.normalLoud)
        {
            isTalking = true;
            isScreaming = false;
        }
        else
        {
            isTalking = false;
            isScreaming = false;
        }
    }

    public void SetThreshHold()
    {
        if(isNormal)
        {
            normalLoud = slider.value;
        }
        else
        {
            screamLoud = slider.value;
        }
    }
}

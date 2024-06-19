using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoudThresh : SoundSetting
{
    public bool isNormal;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        normalLoud = 0.0f;
        screamLoud = 0.0f;
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

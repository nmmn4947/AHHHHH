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
        if (isNormal)
        {
            slider.value = normalLoud;
        }
        else
        {
            slider.value = screamLoud;
        }
            
    }

    private void Update()
    {
        if (MicInput.Loudness > screamLoud)
        {
            isScreaming = true;
            // if scream then talk is also available
        }
        else if (MicInput.Loudness > normalLoud)
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

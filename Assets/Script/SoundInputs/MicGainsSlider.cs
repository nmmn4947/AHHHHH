using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MicGainsSlider : SoundSetting
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0.01f;
    }

    private void Update()
    {
        if(slider.value < 0.01f)
        {
            slider.value = 0.01f;
        }
    }

    public void SetGains()
    {
        GainsValue = slider.value * 100;
    }
}

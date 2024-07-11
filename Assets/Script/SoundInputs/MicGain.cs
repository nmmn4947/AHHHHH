using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MicGain : SoundSetting
{
    private TMP_InputField input;

    private void Start()
    {
        input = GetComponent<TMP_InputField>();
        input.text = GainsValue.ToString();
    }

    private void Update()
    {
        if (float.Parse(input.text) < 1)
        {
            GainsValue = 1.0f;
        }
        GainsValue = float.Parse(input.text);
        Debug.Log("Gains " + GainsValue);
    }
}

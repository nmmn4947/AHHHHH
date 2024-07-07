using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    //ThreshHold
    public static string selectedDevice;
    public static float normalLoud = 0.1f;
    public static float screamLoud = 0.3f;
    //Gains
    public static float GainsValue;
    //CutBgNoise
    public static float loudestBgNoise = 0;

    private void Update()
    {
        Debug.Log("NormLoud " + normalLoud);
        Debug.Log("ScreamLoud " + screamLoud);
    }
}

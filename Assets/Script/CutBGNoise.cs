using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutBGNoise : SoundSetting
{
    public GameObject cuttingText;

    private IEnumerator CutBGSound(float waitTime)
    {
        float keep = waitTime;
        Debug.Log("Start Cutting...");
        cuttingText.SetActive(true);

        while (keep > 0)
        {
            if (loudestBgNoise < MicInput.Loudness)
            {
                loudestBgNoise = MicInput.Loudness;
            }
            keep -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("End - " + loudestBgNoise);
        cuttingText.SetActive(false);
    }

    public void StartCutBGSound(float waitTime)
    {
        StartCoroutine(CutBGSound(waitTime));
    }
}
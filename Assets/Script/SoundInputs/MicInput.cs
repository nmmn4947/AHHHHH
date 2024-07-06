using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    
    public static float Loudness = 0;
    public static float LoudnessTrigger = 0.2f;

    public int sampleWindow = 64;
    public float currentLoud;
    private AudioClip microphoneClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        MicrophoneToAudioClip();

/*      audioSource = GetComponent<AudioSource>();
        audioSource.clip = microphoneClip;
        audioSource.Play();*/
    }

    // Update is called once per frame
    void Update()
    {
        currentLoud = GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
        if (currentLoud < CutBGNoise.loudestBgNoise)
        {
            Loudness = CutBGNoise.loudestBgNoise;
        }
        else
        {
            Loudness = currentLoud * MicGainsSlider.GainsValue;
        }
        Debug.Log(SoundSetting.selectedDevice);
        //Debug.Log("Loud : " + Loudness.ToString());
    }

    
    public void MicrophoneToAudioClip()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.Log("No Microphone in device");
        }
        Debug.Log(SoundSetting.selectedDevice);
        microphoneClip = Microphone.Start(SoundSetting.selectedDevice, true, 10, AudioSettings.outputSampleRate);
    }


    public float GetLoudnessFromMic()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        //compute loudness
        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            //Debug.Log(waveData[i]);
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        //Debug.Log(totalLoudness / sampleWindow);
        return totalLoudness / sampleWindow;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeviceMicDrop : SoundSetting
{
    public TMP_Dropdown micDropdown;
    private List<string> micDevices;
    void Start()
    {
        micDevices = new List<string>(Microphone.devices);
        micDropdown = GetComponent<TMP_Dropdown>();
        micDropdown.AddOptions(micDevices);
        
    }

    void Update()
    {
        selectedDevice = Microphone.devices[micDropdown.value];
    }
}

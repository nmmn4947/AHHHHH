using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoudTrigger : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = MicInput.LoudnessTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        MicInput.LoudnessTrigger = slider.value;
    }
}

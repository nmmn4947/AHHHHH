using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
    public static CamManager Instance;
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _cbmcp = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0.0f;
    }

    public void SetShakeIntensity(float intensity)
    {
        _cbmcp.m_AmplitudeGain = intensity;
    }



    // didn't use
    public IEnumerator CamShake6DWhileStop(float intensity, float duration)
    {
        _cbmcp.m_AmplitudeGain = intensity;
        yield return new WaitForSecondsRealtime(duration);
        _cbmcp.m_AmplitudeGain = 0.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    public int target = 60;

    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
#else
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
#endif
    }
}

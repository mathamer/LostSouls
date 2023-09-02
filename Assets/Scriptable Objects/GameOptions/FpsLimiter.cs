using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    public static FpsLimiter instance;

    public int target = 60;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
#else
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
#endif
    }
}

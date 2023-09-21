using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action MaketaDraggedOntoLostSoul;

    public static void TriggerMaketaDraggedOntoLostSoul()
    {
        if (MaketaDraggedOntoLostSoul != null)
        {
            MaketaDraggedOntoLostSoul.Invoke();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    public static Observer current;
    void Awake()
    {
        current = this;
    }

    public event Action<long> markerSelected;
    public void MarkerSelected(long id)
    {
        if (markerSelected != null)
        {
            markerSelected(id);
        }
    }

    public event Action updateMarker;
    public void UpdateMarker()
    {
        if (updateMarker != null)
        {
            updateMarker();
        }
    }
}

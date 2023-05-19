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

    public event Action changingPositionValues;
    public void ChangingPositionValues()
    {
        if (changingPositionValues != null)
        {
            changingPositionValues();
        }
    }

    public event Action changingScaleValues;
    public void ChangingScaleValues()
    {
        if (changingScaleValues != null)
        {
            changingScaleValues();
        }
    }
}

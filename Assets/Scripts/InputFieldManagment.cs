using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldManagment : MonoBehaviour
{
    [SerializeField] private TMP_InputField x;
    [SerializeField] private TMP_InputField y;
    [SerializeField] private TMP_InputField z;
    private Vector3 values;

    public void SetTexts(Vector3 values)
    {
        x.text = values.x.ToString();
        y.text = values.y.ToString();
        z.text = values.z.ToString();
    }

    public void ReadX(string content)
    {
        values.x = (float) Convert.ToDouble(content);
    }

    public void ReadY(string content)
    {
        values.y = (float)Convert.ToDouble(content);
    }

    public void ReadZ(string content)
    {
        values.z = (float)Convert.ToDouble(content);
    }

    public Vector4 ReadFielads()
    {
        return values;
    }
}

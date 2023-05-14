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

    private void ReadX(string content)
    {
        values.x = (float) Convert.ToDouble(content);
    }

    private void ReadY(string content)
    {
        values.y = (float)Convert.ToDouble(content);
    }

    private void ReadZ(string content)
    {
        values.z = (float)Convert.ToDouble(content);
    }
    

    public void ReadAll(string placeHolder = "")
    {
        ReadX(x.text);
        ReadY(y.text);
        ReadZ(z.text);
    }

    public Vector3 ReadFielads()
    {
        ReadAll();
        return values;
    }
}

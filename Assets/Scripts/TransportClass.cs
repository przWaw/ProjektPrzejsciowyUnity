using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransportClass
{
    public string label;
    public long id;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public List<string> views; 
}

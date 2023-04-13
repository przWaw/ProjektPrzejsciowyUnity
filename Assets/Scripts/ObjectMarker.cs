using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectMarker : MonoBehaviour
{
    public long Id { get; set; }
    private HashSet<string> views;
    private void Start()
    {
        views = new HashSet<string>();    
    }

    public void addView(string name)
    {
        views.Add(name.ToLower());
    }

    public HashSet<string> GetViews()
    {
        return views;
    }
    
    public TransportClass getMarker()
    {
        return new TransportClass
        {
            id = Id,
            position = this.transform.localPosition,
            rotation = this.transform.localEulerAngles,
            scale = this.transform.localScale,
            views = this.views.ToList()
        };
    }
}

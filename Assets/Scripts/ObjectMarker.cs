using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectMarker : MonoBehaviour
{
    public string Label { get; set; }
    public long Id { get; set; }
    private HashSet<string> views;
    private void Start()
    {
        views = new HashSet<string>();    
    }

    public void addView(string name)
    {
        views.Add(name.ToLower().Trim());
    }

    public List<string> GetViews()
    {
        if (views != null) 
        { 
        List<string> sorted = views.ToList();
        sorted.Sort();
        return sorted;
        }
        return null;
    }
    
    public TransportClass getMarker()
    {
        return new TransportClass
        {
            label = Label,
            id = Id,
            position = this.transform.localPosition,
            rotation = this.transform.localEulerAngles,
            scale = this.transform.localScale,
            views = this.views.ToList()
        };
    }
}

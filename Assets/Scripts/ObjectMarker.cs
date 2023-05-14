using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class ObjectMarker : MonoBehaviour
{
    public string Label { get; set; }
    public long Id { get; set; }
    public string Url { get; set; }
    public string ElementType { get; set; }
    private HashSet<string> views;
    private void Start()
    {
        views = new HashSet<string>();    
    }

    public void addView(string name)
    {
        views.Add(name.ToLower().Trim());
    }


    public void AddDetailsView(string prefix)
    {
        views.Clear();
        views.Add(prefix.ToLower() + "_" + Label.ToLower() + "Details");
    }

    public void RemoveDetailsView()
    {
        views.RemoveWhere(s => Regex.Match(s, @".*Details").Success);
    }

    public List<string> GetViews()
    {
        if (views != null) 
        {
            views.Add(ElementType);
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
            url = Url,
            type = ElementType,
            position = this.transform.localPosition,
            rotation = this.transform.localEulerAngles,
            scale = this.transform.localScale,
            views = this.views.ToList()
        };
    }
}

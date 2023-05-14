using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro.Examples;

public class Storage : MonoBehaviour
{
    private GameObject sceneContext;
    private List<ObjectMarker> markers;
    private HashSet<string> viewList;
    private SaveScene savingMethod;
    public string SceneName { get; set; }
    public static Storage storage;

    private void Awake()
    {
        storage = this;
    }

    private void Start()
    {
        markers = new List<ObjectMarker>();
        viewList = new HashSet<string>();
        Observer.current.markerSelected += UnselectAll;
        savingMethod = new SaveScene();
        SceneName = "Scene";
    }

    private string GenerateRandomString(int length = 10)
    {
        string temp = "";
        System.Random radom = new System.Random();
        for (int i = 0; i < length; i++)
        {
            temp += (char)radom.Next(65, 126);
        }

        return temp;
    }

    public void SetSceneContext(GameObject sceneContext)
    {
        this.sceneContext = sceneContext;
    }

    private void LinkMarkersToContext()
    {
        if (this.sceneContext != null)
        {
            foreach (ObjectMarker marker in markers)
            {
                marker.transform.parent = this.sceneContext.transform;
            }
        }
    }

    private void UnlinkMarkersFromContext()
    {
        foreach (ObjectMarker marker in markers)
        {
            marker.transform.parent = null;
        }
    }

    public void AddMarker(ObjectMarker marker)
    {
        markers.Add(marker);
    }

    public void UpdateViews(ObjectMarker marker)
    {
        if (marker != null)
        {
            if (marker.GetViews() != null)
            {
                foreach (var view in marker.GetViews())
                {
                    if (view != null)
                    {
                        viewList.Add(view);
                    }
                }
            }
        }
    }

    public void RemoveDetailsView()
    {
        viewList.RemoveWhere(s => Regex.Match(s, @".*Details").Success);
    }
    private void AddDetailViews(ObjectMarker marker)
    {;
        if (marker.GetViews() != null)
        {
            if (!string.IsNullOrEmpty(marker.Label))
            {
                marker.AddDetailsView(SceneName);
            }
        }
    }

    private void UnselectAll(long id)
    {
        foreach (var marker in markers)
        {
            marker.GetComponent<Dragging>().Unselect();
        }
    }

    public void Save()
    {
        LinkMarkersToContext();
        viewList.Clear();
        foreach (var marker in markers)
        {
            AddDetailViews(marker);
            UpdateViews(marker);
        }
        savingMethod.SaveState(markers, viewList, SceneName);
        UnlinkMarkersFromContext();
    }

    public GameObject findById(long id)
    {
        foreach (var marker in markers)
        {
            if (marker.GetComponent<ObjectMarker>().Id == id)
            {
                return marker.gameObject;
            }
        }
        return null;
    }
}

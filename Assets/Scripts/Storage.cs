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
        SceneName = GenerateRandomString();
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
        foreach (var marker in markers)
        {
            marker.transform.SetParent(this.sceneContext.transform);
        }
    }

    public void AddMarker(ObjectMarker marker)
    {
        markers.Add(marker);
        if (sceneContext != null)
        {
        marker.transform.SetParent(this.sceneContext.transform);
        }
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
    {
        marker.RemoveDetailsView();
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
        RemoveDetailsView();
        foreach (var marker in markers)
        {
            AddDetailViews(marker);
            UpdateViews(marker);
        }
        savingMethod.SaveState(markers, viewList, SceneName);
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

using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private GameObject sceneContext;
    private List<GameObject> markers;
    private HashSet<string> viewList;
    private SaveScene savingMethod;
    public static Storage storage;

    private void Start()
    {
        markers = new List<GameObject>();
        viewList = new HashSet<string>();
        Observer.current.markerSelected += UnselectAll;
        savingMethod = new SaveScene();
        storage = this;
    }

    public void SetSceneContext(GameObject sceneContext)
    {
        this.sceneContext = sceneContext;
        foreach (var marker in markers)
        {
            marker.transform.SetParent(this.sceneContext.transform);
        }
    }

    public void AddMarker(GameObject marker)
    {
        markers.Add(marker);
        if (sceneContext != null)
        {
        marker.transform.SetParent(this.sceneContext.transform);
        }
    }

    public void UpdateViews(GameObject marker)
    {
        if (marker != null)
        {
            ObjectMarker myMarker = marker.GetComponent<ObjectMarker>();
            if (myMarker.GetViews() != null)
            {
                foreach (var view in marker.GetComponent<ObjectMarker>().GetViews())
                {
                    if (view != null)
                    {
                        viewList.Add(view);
                    }
                }
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
        foreach (var marker in markers)
        {
            UpdateViews(marker);
        }
        savingMethod.SaveState(markers, viewList);
    }

    public GameObject findById(long id)
    {
        foreach (var marker in markers)
        {
            if (marker.GetComponent<ObjectMarker>().Id == id)
            {
                return marker;
            }
        }
        return null;
    }
}

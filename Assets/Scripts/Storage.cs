using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Storage : MonoBehaviour
{
    private GameObject sceneContext;
    private List<GameObject> markers;
    private HashSet<string> views;
    private SaveScene savingMethod;

    private void Start()
    {
        markers = new List<GameObject>();
        views = new HashSet<string>();
        Observer.current.markerSelected += UnselectAll;
        savingMethod = new SaveScene();
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
        UpdateViews(marker);
    }

    public void UpdateViews(GameObject marker)
    {
        if (views.Count > 0)
        {
            foreach (var view in marker.GetComponent<ObjectMarker>().GetViews())
            {
                if (view != null)
                {
                    views.Add(view);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) 
        {
            this.Save();
        }
    }

    public void Save()
    {
        savingMethod.SaveState(markers, views);
    }
}

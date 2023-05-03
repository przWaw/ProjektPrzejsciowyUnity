using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSelectedObject : MonoBehaviour
{
    public ObjectMarker Marker { get; private set; }
    
    void Start()
    {
        Observer.current.markerSelected += SetMarkerDetails;
    }

    private void SetMarkerDetails(long id)
    {
        GameObject gameObject = Storage.storage.findById(id);
        if (gameObject != null)
        {
            this.Marker = gameObject.GetComponent<ObjectMarker>();
            Observer.current.UpdateMarker();
        }

    }
}

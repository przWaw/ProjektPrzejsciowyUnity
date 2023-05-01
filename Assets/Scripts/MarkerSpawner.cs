using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarkerSpawner : MonoBehaviour
{
    [SerializeField] private ObjectMarker objectMarker;
    private Storage storage;
    private ObjectMarker lastMarker;
    private long currentId;

    private void Start()
    {
        storage = Storage.storage;   
    }

    public void Spawn()
    {
        lastMarker = Instantiate<ObjectMarker>(objectMarker); 
        lastMarker.GetComponent<ObjectMarker>().Id = currentId;
        SaveToStorage(lastMarker);
    }

    private void SaveToStorage(ObjectMarker marker)
    {
        storage.AddMarker(marker);
        currentId++;
    }
}

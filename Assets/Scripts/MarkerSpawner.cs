using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarkerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectMarker;
    private Storage storage;
    private GameObject lastMarker;
    private long currentId;

    private void Start()
    {
        storage = Storage.storage;   
    }

    public void Spawn()
    {
        lastMarker = Instantiate(objectMarker);
        lastMarker.GetComponent<ObjectMarker>().Id = currentId;
        SaveToStorage(lastMarker);
    }

    private void SaveToStorage(GameObject marker)
    {
        storage.AddMarker(marker);
        currentId++;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveScene
{

    private readonly string saveDirectory = Application.dataPath + "/savedScenes/";
    
    public SaveScene()
    {
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    public void SaveState(List<GameObject> markers, HashSet<string> views)
    {
        string fileName = saveDirectory + "/Scene_";
        long id = 0;
        while (File.Exists(saveDirectory + "/Scene_" + id + ".json"))
        {
            id++;
        }
        fileName = fileName + id + ".json";
        File.WriteAllText(fileName, ConvertToJson(markers, views));
        Debug.Log(fileName);
        id++;
    }

    private string ConvertToJson(List<GameObject> markers, HashSet<string> views)
    {
        SaveObject saveObject = new SaveObject
        {
            views = views.ToList(),
            objects = new List<TransportClass>()
        };
        foreach (var marker in markers)
        {
            if (marker != null)
            {
                saveObject.objects.Add(marker.GetComponent<ObjectMarker>().getMarker());
            }
        }
        return JsonUtility.ToJson(saveObject, true);
    }
    private class SaveObject
    {
        public List<TransportClass> objects;
        public List<string> views;
    }

}

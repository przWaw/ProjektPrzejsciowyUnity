using Dummiesman;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;


public class ContextReader : MonoBehaviour
{
    private string folderPath = string.Empty;
    private List<string> files = new List<string>();
    private GameObject context;

    [SerializeField] private TMP_Dropdown menu;

    private void Start()
    {
        folderPath = Application.dataPath + "/stations/";
        string[] filesInFolder = Directory.GetFiles(folderPath, "*.obj");
        var temp = filesInFolder.ToList<string>();
        foreach (string file in temp)
        {
            //files.Add(GetBaseName(file));
            files.Add(GetNameWithSufix(file));
        }
        menu.options.Clear();
        foreach (string file in files)
        {
            menu.options.Add(new TMP_Dropdown.OptionData(GetBaseName(file)));
        }
        OnOptionChange(0);
    }

    public void OnOptionChange(int index)
    {
        string objPath = folderPath + files[index];
        Destroy(context);
        if (File.Exists(objPath))
        {
            context = new OBJLoader().Load(objPath);
        }
        else
        {
            Debug.Log(objPath);
        }
    }

    private string GetBaseName(string name)
    {
        string temp = name.Substring(name.LastIndexOf("/") + 1);
        return temp.Substring(0, temp.LastIndexOf("."));
    }

    private string GetNameWithSufix(string name)
    {
        return name.Substring(name.LastIndexOf("/") + 1);
    }
}

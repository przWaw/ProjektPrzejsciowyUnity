using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextReader : MonoBehaviour
{
    private string folderPath = "Assets/Resources/stations/";
    private List<string> files = new List<string>();
    private GameObject context;

    [SerializeField] private TMP_Dropdown menu;

    private void Start()
    {
        string[] filesInFolder = Directory.GetFiles(folderPath, "*.obj");
        var temp = filesInFolder.ToList<string>();
        foreach (string file in temp)
        {
            files.Add(GetBaseName(file));
        }
        menu.options.Clear();
        foreach (string file in files)
        {
            menu.options.Add(new TMP_Dropdown.OptionData(file));
        }
        context = Instantiate(Resources.Load("stations/" + files[0], typeof(GameObject)) as GameObject);
    }

    public void OnOptionChange(int index)
    {
        Destroy(context);
        context = Instantiate(Resources.Load("stations/" + files[index], typeof(GameObject))) as GameObject;
    }

    private string GetBaseName(string name)
    {
        string temp = name.Substring(name.LastIndexOf("/") + 1);
        return temp.Substring(0, temp.LastIndexOf("."));
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class SceneNaming : MonoBehaviour
{ 
    public void SetSceneName(string content)
    {
        if (!string.IsNullOrEmpty(content.Trim()))
        {
            Storage.storage.SceneName = content.Trim().Replace(" ", "_");
            UpdateText();
        } else
        {
            this.GetComponent<TMP_InputField>().text = "";
        }
    }

    private void UpdateText()
    {
        this.GetComponent<TMP_InputField>().text = Storage.storage.SceneName;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ViewAdding : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    

    

    public void AddLine(string content)
    {
        if (content == null) return;
        if (content == "") return;
        text.text = text.text + content + "\n";
        this.GetComponent<TMP_InputField>().text = "";
        this.GetComponent<TMP_InputField>().Select();
        this.GetComponent<TMP_InputField>().ActivateInputField();
    }

}

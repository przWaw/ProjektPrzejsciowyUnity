using TMPro;
using UnityEngine;

public class LabelSetter : MonoBehaviour
{
    private ObjectMarker marker;
    void Start()
    {
        Observer.current.markerSelected += SetMarkerDetails;
    }

    private void SetMarkerDetails(long id)
    {
        GameObject gameObject = Storage.storage.findById(id);
        if (gameObject != null)
        {
            this.marker = gameObject.GetComponent<ObjectMarker>();
            UpdateText();
        }
    }

    public void SetLabel(string content)
    {
        if (this.marker != null)
        {
            if (!string.IsNullOrEmpty(content.Trim()))
            {
                marker.Label = content.ToUpper().Trim().Replace(" ", "_");
                UpdateText();
                return;
            }
        }      
        this.GetComponent<TMP_InputField>().text = "";
        
    }
    private void UpdateText()
    {
        this.GetComponent<TMP_InputField>().text = marker.Label;
    }
    
}

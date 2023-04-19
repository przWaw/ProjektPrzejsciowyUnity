using TMPro;
using UnityEngine;

public class LabelSetter : MonoBehaviour
{
    private ObjectMarker marker;
    [SerializeField] private Observer observer;
    void Start()
    {
        observer.markerSelected += SetMarkerDetails;
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
            if (!string.IsNullOrEmpty(content))
            {
                marker.Label = content;
            }
        }
    }
    private void UpdateText()
    {
        this.GetComponent<TMP_InputField>().text = marker.Label;
    }
    
}

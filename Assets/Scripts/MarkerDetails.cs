using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkerDetails : MonoBehaviour
{
    private ObjectMarker marker;
    [SerializeField] private TextMeshProUGUI text;
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
            UpdateViews();
        }

    }

    public void AddViews(string content)
    {
        if (marker != null)
        {
            if (content != null)
            {
                content = content.Trim().Replace(" ", "_");
                if (content.Length > 0) { 
                marker.addView(content);
                UpdateViews();
                    return;
                }
            }
        }
        this.GetComponent<TMP_InputField>().text = "";
        
    }

    public void ShowMove()
    {
        if (marker != null)
        {
            marker.GetComponent<Dragging>().ShowMove();
        }
    }

    public void ShowRotate()
    {
        if (marker != null)
        {
            marker.GetComponent<Dragging>().ShowRotate();
        }
    }

    public void ShowScale()
    {
        if (marker != null)
        {
            marker.GetComponent<Dragging>().ShowScale();
        }
    }

    private void UpdateViews()
    {
        text.text = string.Join('\n', marker.GetViews());
        this.GetComponent<TMP_InputField>().text = "";
    }

    public void FocusAgain(string none)
    {
        this.GetComponent<TMP_InputField>().text = "";
        this.GetComponent<TMP_InputField>().Select();
        this.GetComponent<TMP_InputField>().ActivateInputField();
    }
}
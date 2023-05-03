using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkerDetails : MonoBehaviour
{
    private ObjectMarker marker;
    [SerializeField] UpdateSelectedObject updater;
    [SerializeField] private TMP_InputField labelText;
    [SerializeField] private TMP_InputField urlText;
    [SerializeField] private InputFieldManagment inputFields;
    private bool move, scale = false;
    void Start()
    {
        Observer.current.updateMarker += SetMarkerDetails;
        inputFields.gameObject.SetActive(false);
    }

    private void SetMarkerDetails()
    {
        marker = updater.Marker;
        UpdateLabel();
        UpdateUrl();
        inputFields.gameObject.SetActive(false);
        //UpdateViews();
    }

    //public void AddViews(string content)
    //{
    //    if (marker != null)
    //    {
    //        if (content != null)
    //        {
    //            content = content.Trim().Replace(" ", "_");
    //            if (content.Length > 0) { 
    //            marker.addView(content);
    //            UpdateViews();
    //                return;
    //            }
    //        }
    //    }
    //    viewsText.text = "";
        
    //}
    //private void UpdateViews()
    //{
    //    text.text = string.Join('\n', marker.GetViews());
    //    viewsText.text = "";
    //}


    public void SetUrl(string content)
    {
        if (this.marker != null)
        {
            if (!string.IsNullOrEmpty(content.Trim()))
            {
                marker.Url = content;
                UpdateUrl();
            }
        }
        urlText.text = "";

    }
    
    private void UpdateUrl()
    {
        urlText.text = marker.Url;
    }

    public void SetLabel(string content)
    {
        if (this.marker != null)
        {
            if (!string.IsNullOrEmpty(content.Trim()))
            {
                marker.Label = content.ToUpper().Trim().Replace(" ", "_");
                UpdateLabel();
                return;
            }
        }
        labelText.text = "";

    }

    private void UpdateLabel()
    {
        labelText.text = marker.Label;
    }

    public void ShowMove()
    {
        if (marker != null)
        {
            marker.GetComponent<Dragging>().ShowMove();
            VisualisePosition();
        }
    }

    public void ShowScale()
    {
        if (marker != null)
        {
            marker.GetComponent<Dragging>().ShowScale();
            VisualiseScale();
        }
    }

    private void VisualisePosition()
    {
        inputFields.gameObject.SetActive(true);
        move = true;
        scale = false;
        inputFields.SetTexts(marker.transform.localPosition);
    }

    private void VisualiseScale()
    {
        inputFields.gameObject.SetActive(true);
        move = false;
        scale = true;
        inputFields.SetTexts(marker.transform.localScale);
    }

    public void ChangePosition(string content)
    {
        if (move)
        {
            marker.transform.localPosition = inputFields.ReadFielads();
            VisualisePosition();
        }
        if(scale)
        {
            marker.transform.localScale = inputFields.ReadFielads();
            VisualiseScale();
        }
    }

    public void FocusAgain(string none)
    {
        this.GetComponent<TMP_InputField>().text = "";
        this.GetComponent<TMP_InputField>().Select();
        this.GetComponent<TMP_InputField>().ActivateInputField();
    }
}
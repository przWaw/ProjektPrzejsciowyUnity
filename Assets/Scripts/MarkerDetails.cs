using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarkerDetails : MonoBehaviour
{
    private ObjectMarker marker;
    [SerializeField] UpdateSelectedObject updater;
    [Header("InputFields")]
    [SerializeField] private TMP_InputField urlText;
    [SerializeField] private TMP_InputField labelText;
    [Header("Visibilities")]
    [SerializeField] private InputFieldManagment inputFields;
    [SerializeField] private GameObject menu;
    [Header("Buttons")]
    [SerializeField] private Button input;
    [SerializeField] private Button output;
    [SerializeField] private Button model;
    private bool move, scale = false;
    private bool visibleMove, visibleScale = false;
    void Start()
    {
        Observer.current.updateMarker += SetMarkerDetails;
        Observer.current.changingPositionValues += VisualisePosition;
        inputFields.gameObject.SetActive(false);
        menu.SetActive(false);
    }

    private void SetMarkerDetails()
    {
        visibleMove = false;
        visibleScale = false;
        menu.SetActive(true);
        marker = updater.Marker;
        UpdateLabel();
        UpdateUrl();
        UpdateButtons();
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
                return;
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
        visibleScale = false;
        if (visibleMove == false)
        { 
            if (marker != null)
            {
                marker.GetComponent<Dragging>().ShowMove();
                VisualisePosition();
                visibleMove = true;
            }
        }
        else if (visibleMove == true)
        {
            marker.GetComponent<Dragging>().HideAll();
            visibleMove = false;
            inputFields.gameObject.SetActive(false);
        }
    }

    public void ShowScale()
    {
        visibleMove = false;
        if (visibleScale == false)
        {
            if (marker != null)
            {
                marker.GetComponent<Dragging>().ShowScale();
                VisualiseScale();
                visibleScale = true;
            }
        }
        else if (visibleScale == true)
        {
            marker.GetComponent<Dragging>().HideAll();
            visibleScale = false;
            inputFields.gameObject.SetActive(false);
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

    public void SetElementType(string type)
    {
        if (this.marker != null)
        {
            this.marker.ElementType = type;
        }
    }

    private void ElementInput() => SetElementType(ElementType.INPUT);
    private void ElementOutput() => SetElementType(ElementType.OUTPUT);
    private void ElementModel() => SetElementType(ElementType.MODEL);

    public void ColoredElementInput()
    {
        UncolorButtons();
        input.GetComponent<Image>().color = Color.grey;
        ElementInput();
    }
    public void ColoredElementOutput()
    {
        UncolorButtons();
        output.GetComponent<Image>().color = Color.grey;
        ElementOutput();
    }
    public void ColoredElementModel()
    {
        UncolorButtons();
        model.GetComponent<Image>().color = Color.grey;
        ElementModel();
    }

    private void UncolorButtons()
    {
        input.GetComponent<Image>().color = Color.white;
        output.GetComponent<Image>().color = Color.white;
        model.GetComponent<Image>().color = Color.white;
    }

    private void UpdateButtons()
    {
        switch (this.marker.ElementType)
        {
            case ElementType.INPUT: 
                ColoredElementInput();
                break;
            case ElementType.OUTPUT: 
                ColoredElementOutput(); 
                break;
            case ElementType.MODEL:
                ColoredElementModel();
                break;
            default:
                UncolorButtons();
                break;
        }
    }

    public void FocusAgain(string none)
    {
        this.GetComponent<TMP_InputField>().text = "";
        this.GetComponent<TMP_InputField>().Select();
        this.GetComponent<TMP_InputField>().ActivateInputField();
    }
}
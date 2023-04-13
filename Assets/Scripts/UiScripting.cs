using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScripting : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    private Dragging marker;
    void Start()
    {
        marker = this.GetComponent<Dragging>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (marker.selected)
        {
            visuals.SetActive(true);
        } 
        if (!marker.selected)
        {
            visuals.SetActive(false);
        }
    }
}

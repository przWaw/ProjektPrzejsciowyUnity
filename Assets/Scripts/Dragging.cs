using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    private Vector3 offsetFromScreeen;
    private float zCoord;
    private long id;
    private bool visualsOn;
    public bool selected;
    private BoxCollider myCollider;
    private MovingVisualControl control;

    private void Start()
    {
        id = this.GetComponent<ObjectMarker>().Id;
        Observer.current.markerSelected += Select;
        control = visuals.GetComponent<MovingVisualControl>();
        myCollider = this.GetComponent<BoxCollider>();
        visuals.SetActive(false);
        visualsOn = false;
    }

    public void ShowMove()
    {
       control.ShowMove();
       visualsOn = true;
       myCollider.enabled = false;
    }

    public void ShowScale()
    {
        control.ShowScale();
        visualsOn = true;
        myCollider.enabled = false;
    }

    public void HideAll()
    {
        control.HideAll();
        visualsOn = false;
        myCollider.enabled = true;
    }

    private void Select(long id)
    {
       
        if (this.id == id)
        {
            selected = true;
            visuals.SetActive(true);
            control.HideAll();
        }
       
    }

    public void Unselect()
    {
        selected = false;
        control.HideAll();
        visualsOn = false;
        myCollider.enabled = true;
        visuals.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!selected)
        {
            Observer.current.MarkerSelected(id);
        }
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offsetFromScreeen = transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePosition);

    }

    private void OnMouseDrag()
    {
        if (selected & !visualsOn)
        {
        transform.position = GetMouseAsWorldPoint() + offsetFromScreeen;
        }
    }

    private void OnDestroy()
    {
        Observer.current.markerSelected -= Select;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    private Vector3 offsetFromScreeen;
    private float zCoord;
    public bool selected;
    private long id;

    private void Start()
    {
        id = this.GetComponent<ObjectMarker>().Id;
        Observer.current.markerSelected += Select;
    }


    private void Select(long id)
    {
        if (this.GetComponent<ObjectMarker>().Id == id)
        {
        selected = true;
        }
    }

    public void Unselect()
    {
        selected = false;
    }

    private void OnMouseDown()
    {
        Observer.current.MarkerSelected(id);
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
        if (selected)
        {
        transform.position = GetMouseAsWorldPoint() + offsetFromScreeen;
        }
    }
}

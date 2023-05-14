using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisMovement : MonoBehaviour
{
    [SerializeField] Transform motherCube;
    [SerializeField] Axis axis;
    private bool wantsToMove = false;
    private Vector3 offset = Vector3.zero;
    private void OnMouseDown()
    {
        wantsToMove = true;
        var direction = Camera.main.transform.forward;
        if(axis == Axis.Z)
        {
            direction.z = 0;
        }
        if(axis == Axis.X)
        {
            direction.x = 0;
        }
        if(axis == Axis.Y)
        { 
            direction.y = 0;
        }
        var planeNormal = direction;

        var pointerPosition = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(pointerPosition);
        var plane = new Plane(planeNormal, this.transform.position);

        if (!plane.Raycast(ray, out float enter))
        {
            Debug.LogError("I hope that you wouldn't click parallel to plane...");
            return;
        }

        var point = ray.GetPoint(enter);
        offset = motherCube.localPosition - point;
    }

    private void OnMouseUp()
    {
        wantsToMove = false;
    }

    private void OnMouseDrag()
    {
        var direction = Camera.main.transform.forward;
        if(axis == Axis.Z)
        {
            direction.z = 0;
        }
        if(axis == Axis.X)
        {
            direction.x = 0;
        }
        if(axis == Axis.Y)
        { 
            direction.y = 0;
        }
        var planeNormal = direction;

        if ( wantsToMove ) 
        {
            var pointerPosition = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(pointerPosition);
            var plane = new Plane(planeNormal, this.transform.position);

            if (!plane.Raycast(ray, out float enter))
            {
                Debug.LogError("I hope that you wouldn't click parallel to plane...");
                return;
            }

            var point = ray.GetPoint(enter);
            var temp = this.transform.position;
            if(axis == Axis.Z)
            {
            temp.z = point.z + offset.z;
            }
            if(axis == Axis.X)
            {
                temp.x = point.x + offset.x;
            }
            if(axis == Axis.Y)
            {
                temp.y = point.y + offset.y;
            }
            motherCube.position = temp;
            Observer.current.ChangingPositionValues();
        }
    }
}

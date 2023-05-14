using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisScaling : MonoBehaviour
{
    [SerializeField] Transform motherCube;
    [SerializeField] Axis axis;
    private bool wantsToScale = false;
    private Vector3 point = Vector3.zero;

    private void OnMouseDown()
    {
        wantsToScale = true;
        var direction = Camera.main.transform.forward;
        if (axis == Axis.Z)
        {
            direction.z = 0;
        }
        if (axis == Axis.X)
        {
            direction.x = 0;
        }
        if (axis == Axis.Y)
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

        point = ray.GetPoint(enter);
    }

    private void OnMouseUp()
    {
        wantsToScale = false;
    }

    private void OnMouseDrag()
    {
        var direction = Camera.main.transform.forward;
        if (axis == Axis.Z)
        {
            direction.z = 0;
        }
        if (axis == Axis.X)
        {
            direction.x = 0;
        }
        if (axis == Axis.Y)
        {
            direction.y = 0;
        }
        var planeNormal = direction;

        if (wantsToScale)
        {
            var pointerPosition = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(pointerPosition);
            var plane = new Plane(planeNormal, this.transform.position);

            if (!plane.Raycast(ray, out float enter))
            {
                Debug.LogError("I hope that you wouldn't click parallel to plane...");
                return;
            }

            var newPoint = ray.GetPoint(enter);

            var shift = newPoint - point;
            var temp = motherCube.localScale;
            if (axis == Axis.X)
            {
                temp.x += shift.x;
            }
            if (axis == Axis.Y)
            {
                temp.y += shift.y;
            }
            if (axis == Axis.Z)
            {
                temp.z += shift.z;
            }
            motherCube.localScale = temp;
            point = newPoint;
        }
    }
}

using UnityEngine;

public class ElementToucher : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log(this.name + " touched");
    }
}

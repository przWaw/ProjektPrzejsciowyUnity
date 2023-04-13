using UnityEngine;

public class FaceCreator : MonoBehaviour
{

    private Transform playerCamera;

    private void Start()
    {
        playerCamera = CreatorMovement.camPosition;
    }

    void Update()
    {
        this.transform.LookAt(playerCamera.transform); 
    }
}

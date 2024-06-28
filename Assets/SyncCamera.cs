using UnityEngine;

public class SyncCameras : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera beaconCamera;

    void LateUpdate()
    {
        if (mainCamera != null && beaconCamera != null)
        {
            beaconCamera.transform.position = mainCamera.transform.position;
            beaconCamera.transform.rotation = mainCamera.transform.rotation;
        }
    }
}
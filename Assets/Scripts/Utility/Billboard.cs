using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject cam;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag(Constants.CAMERA_TAG);
    }

    private void LateUpdate()
    {
        Vector3 cameraDirection = transform.position + cam.transform.forward;

        transform.LookAt(cameraDirection);
    }
}

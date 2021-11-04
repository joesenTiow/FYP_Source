using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mostly used for texts. Attach to gameObject to make it face the camera at all times.
public class FaceCamera : MonoBehaviour
{
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.transform);
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
    }
}

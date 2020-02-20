using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform body;
    float xRotation = 0f;
    public int rotateUpClamp;
    public int rotateDownClamp;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation , rotateUpClamp,rotateDownClamp);
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        body.Rotate(Vector3.up * mouseX);
    }
}

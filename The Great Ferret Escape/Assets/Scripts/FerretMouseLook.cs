using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerretMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform ferretBody;
    float xRotation = 0f;

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
        xRotation = Mathf.Clamp(xRotation , -15,15);
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        ferretBody.Rotate(Vector3.up * mouseX);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSencitivity = 100f;
    public Transform playerBody;
    float xRotation = 0.0f;

    public bool enabled = false;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartTraining += startMouseLook;
        EventManager.PouseGame += stopMouseLook;
    }
    private void startMouseLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        enabled = true;
    }
    private void stopMouseLook()
    {
        
        Cursor.lockState = CursorLockMode.None;
        enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (enabled) { 
            float mouseX = Input.GetAxis("Mouse X") * mouseSencitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSencitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}

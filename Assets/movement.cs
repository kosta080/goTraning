using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public bool movementEnabled = false;
    public CharacterController controller;
    public float speed = 20f;
    public float gravity = -9.8f;
    public float jumpHeight = 2;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask barierMask;

    Vector3 velocity;
    bool isGounded;

    public GameObject deployPoint;
    // Update is called once per frame
    private void Start()
    {
        deployPoint = GameObject.Find("Deploy here");
        Invoke("setDeployPosition", 0.2f);
        EventManager.StartTraining += EnabledMovement;
        EventManager.PouseGame += DisableMovement;
    }
    public void setDeployPosition()
    {
        //Debug.Log(controller.transform.position);
        //Debug.Log(deployPoint.transform.position);
        controller.transform.position = deployPoint.transform.position;
        //Debug.Log(controller.transform.position);
        //Debug.Log(deployPoint.transform.position);
        //controller.transform.rotation = deployPoint.transform.rotation;

        //movementEnabled = true;
        
    }
    private void EnabledMovement()
    {
        movementEnabled = true;
    }
    private void DisableMovement()
    {
        movementEnabled = false;
    }
    void Update()
    {
        if (!movementEnabled) return;

        isGounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGounded && velocity.y < 0)
        {
            //velocity.y = -1f;
        }
       

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //Debug.Log(x + " " + z);
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

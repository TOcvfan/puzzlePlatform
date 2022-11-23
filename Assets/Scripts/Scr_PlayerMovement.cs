using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed, mouseSensitivity;

    [SerializeField]
    private CharacterController charControl;

    [SerializeField]
    private Transform mainCamera;

    private float axisVertical;
    private float axisHorizontal;
    private float mouseInputX;
    private float mouseInputY;

    [SerializeField]
    private float jumpHeight, fallSpeed;
    

    private Vector3 moveDir;

    private float xRotation;

    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {

        myTransform = transform;

        Cursor.lockState = CursorLockMode.Locked;

        charControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        moveDir = new Vector3(0, moveDir.y, 0);

        mouseInputX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseInputY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseInputY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        mainCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        myTransform.Rotate(Vector3.up * mouseInputX);

        axisHorizontal = Input.GetAxisRaw("Horizontal");
        axisVertical = Input.GetAxisRaw("Vertical");

        if (axisVertical > .1f || axisVertical < -.1f)
            moveDir += myTransform.forward * axisVertical;
        
        if (axisHorizontal > .1f || axisHorizontal < -.1f)
            moveDir += myTransform.right * axisHorizontal;
        
        moveDir.x *= moveSpeed * Time.deltaTime;
        moveDir.z *= moveSpeed * Time.deltaTime;

        charControl.Move(moveDir);

        if (charControl.isGrounded)
        {
            print("Are we grounded?");
            if (Input.GetButtonDown("Jump"))
            {
                print("Yass! Queen");
                moveDir.y = jumpHeight;
            }
        }
        else
        {
            print("Not Grounded");
            if (moveDir.y > -1)
            {
                print("Fall damnit");
                moveDir.y -= fallSpeed * Time.deltaTime;
            }
        }

    }
}
